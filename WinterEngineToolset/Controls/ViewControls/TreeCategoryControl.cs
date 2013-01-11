using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.DataLayer.Contexts;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.Controls.ControlHelpers;
using WinterEngine.Toolset.DataLayer.Repositories;
using WinterEngine.Toolset.Enumerations;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.GameObjects;
using WinterEngine.Toolset.Factories;
using WinterEngine.Toolset.Helpers;
using WinterEngine.Toolset.ExtendedEventArgs;

namespace WinterEngine.Toolset.Controls.ViewControls
{
    public partial class TreeCategoryControl : UserControl
    {
        #region Constants

        // Minimum and maximum lengths category names can be.
        private const int MinCategoryNameLength = 1;
        private const int MaxCategoryNameLength = 32;

        #endregion

        #region Fields

        private ResourceTypeEnum _resourceTypeEnum;
        private GameObject _activeGameObject;
        private TreeNode _activeTreeNode;
        
        #endregion

        #region Events / Delegates

        public event EventHandler<GameObjectEventArgs> OnOpenObject;

        #endregion

        #region Properties

        [Description("The resource type to add the category to.")]
        [Browsable(true)]
        public ResourceTypeEnum WinterObjectResourceType
        {
            get { return _resourceTypeEnum; }
            set { _resourceTypeEnum = value; }
        }

        /// <summary>
        /// Gets or sets the active game object.
        /// </summary>
        public GameObject ActiveGameObject
        {
            get { return _activeGameObject; }
            set 
            {
                _activeTreeNode.Tag = value;
                _activeGameObject = value; 
            }
        }

        #endregion

        #region Constructors

        public TreeCategoryControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Fires the OnOpenObject event. Subscribed events should load the object passed
        /// into the toolset.
        /// </summary>
        private void LoadObject(GameObjectEventArgs eventArgs)
        {
            _activeTreeNode = treeView.SelectedNode;
            OnOpenObject(this, eventArgs);
        }

        /// <summary>
        /// Business layer validation for adding a new category.
        /// </summary>
        /// <param name="inputText"></param>
        /// <returns></returns>
        private bool ValidationMethod(string inputText)
        {
            bool isValid = true;

            if (inputText.Length < MinCategoryNameLength) isValid = false;
            if (inputText.Length > MaxCategoryNameLength) isValid = false;

            return isValid;
        }

        /// <summary>
        /// Method is fired when a category is added successfully.
        /// </summary>
        /// <param name="inputText"></param>
        private void AddCategorySuccessMethod(string inputText)
        {
            bool success;
            try
            {
                using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
                {
                    ResourceCategory resourceCategory = new ResourceCategory();
                    resourceCategory.ResourceName = inputText;
                    resourceCategory.ResourceTypeID = (int)WinterObjectResourceType;

                    success = repo.AddResourceCategory(resourceCategory);
                    RefreshTreeView();
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error adding specified category. (Method: AddCategorySuccessMethod).", ex);
            }
        }

        /// <summary>
        /// Method is fired when a category is renamed successfully.
        /// </summary>
        /// <param name="inputText"></param>
        private void RenameCategorySuccessMethod(string inputText)
        {
            try
            {
                using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
                {
                    ResourceCategory resourceCategoryDTO = treeView.SelectedNode.Tag as ResourceCategory;
                    resourceCategoryDTO.ResourceName = inputText;
                    repo.UpdateResourceCategory(resourceCategoryDTO);

                    RefreshTreeView();
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error renaming specified category. (Method: RenameCategorySuccessMethod).", ex);
            }
        }

        /// <summary>
        /// Handles popping up a modal dialog box which asks user to input a category name.
        /// Once a category name has been entered, the database will be updated with the new category.
        /// The tree view is refreshed to show the change.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            InputMessageBox inputBox = new InputMessageBox("Enter the category's name.", "New Category", MinCategoryNameLength, MaxCategoryNameLength, ValidationMethod, AddCategorySuccessMethod, "Category Name");
            inputBox.ShowDialog();
        }

        #endregion

        #region Tree view helpers

        /// <summary>
        /// Populates a tree view with categories from the database.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="resourceType"></param>
        /// <param name="generateUncategorizedCategory"></param>
        public void PopulateTreeViewCategories()
        {
            
            // Retrieve list of ResourceCategoryDTO from the database by way of the data access layer and repositories.
            List<ResourceCategory> categoryList = new List<ResourceCategory>();
            using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
            {
                categoryList = repo.GetAllResourceCategoriesByResourceType(WinterObjectResourceType);
            }

            foreach (ResourceCategory category in categoryList)
            {
                TreeNode treeNode = new TreeNode(category.ResourceName);
                treeNode.Name = "" + category.ResourceCategoryID;
                treeNode.Tag = category;
                treeView.Nodes[0].Nodes.Add(treeNode);
            }

            treeView.Sort();
            treeView.ExpandAll();
        }


        /// <summary>
        /// Populates the tree view with WinterObjectDTO objects.
        /// Note that you must populate tree view categories first or none of these will be added.
        /// </summary>
        public void PopulateTreeViewObjects()
        {
            // Build a list of objects using the WinterObjectFactory
            WinterObjectFactory factory = new WinterObjectFactory();
            List<GameObject> objectList = factory.GetAllFromDatabase(WinterObjectResourceType);

            TreeNodeCollection nodeCollection = treeView.Nodes[0].Nodes;
            // Get list of DTOs from the tag of tree nodes
            List<ResourceCategory> resourceDTOList = GetTreeNodeTagResourceDTOs(nodeCollection);

            foreach (GameObject currentObject in objectList)
            {
                TreeNode treeNode = new TreeNode(currentObject.Name);
                treeNode.Tag = currentObject;

                // Find the first category that matches this object's category ID. There should only be one since CategoryID is a primary key.
                ResourceCategory category = resourceDTOList.FirstOrDefault(val => val.ResourceCategoryID == currentObject.ResourceCategoryID);

                // Unable to find category. Move this object to the first category on the list.
                if (category == null)
                {
                    // If there are no categories at all, we won't load the object.
                    if (ReferenceEquals(treeView.Nodes[0].Nodes, null))
                    {
                        treeView.Nodes[0].Nodes[0].Nodes.Add(treeNode);
                    }
                }
                // Otherwise we found the category. Add the node to it.
                else
                {
                    TreeNode[] treeNodeSearch = nodeCollection.Find("" + category.ResourceCategoryID, false);
                    treeNodeSearch[0].Nodes.Add(treeNode);
                }
            }

        }

        /// <summary>
        /// Clears out all nodes on the treeView and repopulates it based on data from the database.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="resourceType"></param>
        public void RefreshTreeView()
        {
            treeView.Nodes[0].Nodes.Clear();
            PopulateTreeViewCategories();
            PopulateTreeViewObjects();
        }

        /// <summary>
        /// Clears out all nodes on the treeView, leaving only the root node
        /// </summary>
        public void UnloadTreeView()
        {
            treeView.Nodes[0].Nodes.Clear();
        }

        /// <summary>
        /// Method used by children to refresh the tree view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RefreshTreeView(object sender, EventArgs e)
        {
            RefreshTreeView();
        }

        /// <summary>
        /// Retrieves a list of tags from a tree node collection
        /// </summary>
        /// <param name="treeNodeCollection"></param>
        /// <returns></returns>
        private List<ResourceCategory> GetTreeNodeTagResourceDTOs(TreeNodeCollection treeNodeCollection)
        {
            List<ResourceCategory> objectList = new List<ResourceCategory>();

            foreach (TreeNode currentObject in treeNodeCollection)
            {
                ResourceCategory tag = currentObject.Tag as ResourceCategory;
                objectList.Add(tag);
            }

            return objectList;
        }

        /// <summary>
        /// Cycles through all object nodes and refreshes their names,
        /// based on the GameObject located on their "Tag" object
        /// </summary>
        public void RefreshNodeNames()
        {
            // Loop through all categories
            foreach (TreeNode category in treeView.Nodes[0].Nodes)
            {
                // Loop through all game objects contained inside a category
                foreach (TreeNode gameObjectNode in category.Nodes)
                {
                    if (!Object.ReferenceEquals(gameObjectNode.Tag, null))
                    {
                        GameObject gameObject = gameObjectNode.Tag as GameObject;
                        gameObjectNode.Text = gameObject.Name;
                    }
                }
            }
        }

        #endregion

        #region Tree view events

        /// <summary>
        /// Event handler for node selection (single click)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Highlight the selected node on all clicks.
            treeView.SelectedNode = e.Node;
        }

        /// <summary>
        /// Event handler for opening objects (double click)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Selected node was the root node.
            if (e.Node == treeView.TopNode)
            {
            }
            // Selected node was a category
            else if (e.Node.Parent == treeView.TopNode)
            {
            }
            // Selected node was an object
            else
            {
                GameObjectEventArgs eventArgs = new GameObjectEventArgs();
                eventArgs.GameObject = e.Node.Tag as GameObject;
                _activeGameObject = eventArgs.GameObject;
                LoadObject(eventArgs);
            }
        }

        /// <summary>
        /// Sets the root node name to match the name of the resource type enumeration.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeCategoryControl_Load(object sender, EventArgs e)
        {
            treeView.Nodes[0].Text = _resourceTypeEnum.ToString();
        }

        #endregion

        #region Context menu events
        
        /// <summary>
        /// Populates the context menu when the user right clicks a node on the tree view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripNodes_Opening(object sender, CancelEventArgs e)
        {
            contextMenuStripNodes.Items.Clear();

            if (treeView.SelectedNode == treeView.TopNode)
            {
                ToolStripItem createCategory = contextMenuStripNodes.Items.Add("Create Category");
                createCategory.Click += new EventHandler(contextMenuStripNodes_CreateCategory);
            }
            // Category node was selected. We know this because all categories fall under
            // the root node.
            else if (treeView.SelectedNode.Parent == treeView.TopNode)
            {
                // Add options for category context menu
                ToolStripItem createObjectItem = contextMenuStripNodes.Items.Add("Create " + WinterObjectResourceType.ToString());
                contextMenuStripNodes.Items.Add("-");
                ToolStripItem renameCategoryItem = contextMenuStripNodes.Items.Add("Rename");
                ToolStripItem deleteCategoryItem = contextMenuStripNodes.Items.Add("Delete Category");

                // Add events to each item click event
                createObjectItem.Click += new EventHandler(contextMenuStripNodes_CreateObject);
                renameCategoryItem.Click += new EventHandler(contextMenuStripNodes_RenameCategory);
                deleteCategoryItem.Click += new EventHandler(contextMenuStripNodes_DeleteCategory);

            }
            // Otherwise, an object node was selected.
            else
            {
                ToolStripItem openObjectItem = contextMenuStripNodes.Items.Add("Open");
                ToolStripItem deleteObjectItem = contextMenuStripNodes.Items.Add("Delete " + WinterObjectResourceType.ToString());

                openObjectItem.Click += new EventHandler(contextMenuStripNodes_OpenObject);
                deleteObjectItem.Click += new EventHandler(contextMenuStripNodes_DeleteObject);
            }
        }

        /// <summary>
        /// Event handler for creating a new object from context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripNodes_CreateObject(object sender, EventArgs e)
        {
            NewObjectEntry newObjectEntryForm = new NewObjectEntry(WinterObjectResourceType, treeView.SelectedNode.Tag as ResourceCategory);
            newObjectEntryForm.Text = "New " + WinterObjectResourceType.ToString();
            newObjectEntryForm.RefreshParentGUI += new EventHandler(RefreshTreeView);
            newObjectEntryForm.ShowDialog();
        }

        /// <summary>
        /// Event handler for opening an object from context menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripNodes_OpenObject(object sender, EventArgs e)
        {
            GameObjectEventArgs eventArgs = new GameObjectEventArgs();
            eventArgs.GameObject = treeView.SelectedNode.Tag as GameObject;
            _activeGameObject = eventArgs.GameObject;

            LoadObject(eventArgs);
        }

        /// <summary>
        /// Event handler for deleting an object from context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripNodes_DeleteObject(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this " + WinterObjectResourceType.ToString().ToLower() + "?", "Delete " + WinterObjectResourceType.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            // User chose to delete the category. Remove all contained objects and delete the category.
            if (result == DialogResult.Yes)
            {
                GameObject obj = treeView.SelectedNode.Tag as GameObject;
                try
                {
                    // Remove this object from the database
                    WinterObjectFactory factory = new WinterObjectFactory();
                    factory.DeleteFromDatabase(obj.Resref, WinterObjectResourceType);
                    
                    RefreshTreeView();
                    treeView.SelectedNode = treeView.TopNode;
                }
                catch (Exception ex)
                {
                    ErrorHelper.ShowErrorDialog("Error deleting specified " + WinterObjectResourceType.ToString() + " (Method: contextMenuStripNodes_DeleteObject).\n\n", ex);
                }
            }
        }

        /// <summary>
        /// Event handler for creating a category from context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripNodes_CreateCategory(object sender, EventArgs e)
        {
            buttonAddCategory.PerformClick();
        }

        /// <summary>
        /// Event handler for renaming a category from context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripNodes_RenameCategory(object sender, EventArgs e)
        {
            InputMessageBox inputBox = new InputMessageBox("Please enter a new category name.", "Rename Category", MinCategoryNameLength, MaxCategoryNameLength, ValidationMethod, RenameCategorySuccessMethod, treeView.SelectedNode.Text);
            inputBox.Show();
        }

        /// <summary>
        /// Event handler for deleting a category from context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripNodes_DeleteCategory(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this category? All contained objects will be deleted.", "Delete Category", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            // User chose to delete the category. Remove all contained objects and delete the category.
            if (result == DialogResult.Yes)
            {
                ResourceCategory category = treeView.SelectedNode.Tag as ResourceCategory;

                try
                {
                    // Remove the objects contained inside this category from the database
                    WinterObjectFactory factory = new WinterObjectFactory();
                    factory.DeleteFromDatabaseByCategory(category, WinterObjectResourceType);
                    
                    // Remove the category from the database
                    using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
                    {
                        repo.DeleteResourceCategory(category);
                    }
                    RefreshTreeView();
                    treeView.SelectedNode = treeView.TopNode;
                }
                catch (Exception ex)
                {
                    ErrorHelper.ShowErrorDialog("Error deleting specified category (Method: contextMenuStripNodes_DeleteCategory).", ex);
                }

            }
        }

        /// <summary>
        /// Handles key input when the treeview is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_KeyDown(object sender, KeyEventArgs e)
        {
            // Delete key was pressed.
            if (e.KeyCode == Keys.Delete)
            {
                TreeNode selectedNode = treeView.SelectedNode;

                // Be sure that a node is selected
                if (!Object.ReferenceEquals(treeView.SelectedNode, null))
                {
                    // Root node selected - do nothing.
                    if (selectedNode == treeView.TopNode)
                    {
                    }
                    // Parent is the root node. User selected a category node.
                    else if (selectedNode.Parent == treeView.TopNode)
                    {
                        contextMenuStripNodes_DeleteCategory(this, null);
                    }
                    // Otherwise an object node was selected.
                    else
                    {
                        contextMenuStripNodes_DeleteObject(this, null);
                    }
                }
            }
        }

        #endregion



    }
}
