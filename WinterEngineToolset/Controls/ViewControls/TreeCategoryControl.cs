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
        public ResourceTypeEnum GameObjectResourceType
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

        /// <summary>
        /// Gets or sets the active tree node.
        /// The previous tree node, if available, is set to regular font.
        /// The new tree node's test is set to bold.
        /// </summary>
        private TreeNode ActiveTreeNode
        {
            get { return _activeTreeNode; }
            set
            {
                if (!Object.ReferenceEquals(_activeTreeNode, null))
                {
                    _activeTreeNode.NodeFont = new Font(treeView.Font, FontStyle.Regular);
                }
                 
                _activeTreeNode = value;
                _activeTreeNode.NodeFont = new Font(treeView.Font, FontStyle.Bold);
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
            ActiveTreeNode = treeView.SelectedNode;
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
                    resourceCategory.ResourceTypeID = (int)GameObjectResourceType;

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
        /// Clears out all nodes on the treeView and repopulates it based on data from the database.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="resourceType"></param>
        public void RefreshTreeView()
        {
            GameObjectFactory factory = new GameObjectFactory();
            
            using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
            {
                // Retrieve all resource categories
                List<ResourceCategory> resourceCategories = repo.GetAllResourceCategories();
                
                // Loop through each resource category
                foreach (ResourceCategory category in resourceCategories)
                {
                    // Generate the category's key (its ID in the database)
                    string categoryKey = Convert.ToString(category.ResourceCategoryID);

                    TreeNode[] categoryNodesList = treeView.Nodes[0].Nodes.Find(categoryKey, false);
                    TreeNode categoryTreeNode;

                    // Category doesn't exist - create it in the tree
                    if (categoryNodesList.Length <= 0)
                    {
                        categoryTreeNode = new TreeNode();
                        categoryTreeNode.Name = categoryKey;
                        categoryTreeNode.Text = category.ResourceName;
                        treeView.Nodes[0].Nodes.Add(categoryTreeNode);
                    }
                    // Category exists - update its text
                    else
                    {
                        categoryTreeNode = categoryNodesList[0];
                        categoryTreeNode.Text = category.ResourceName;
                    }

                    // Update the category object located on the object's tag
                    categoryTreeNode.Tag = category;

                    // Add object nodes to the category if they do not exist already.
                    List<GameObject> gameObjects = factory.GetAllFromDatabaseByResourceCategory(category, GameObjectResourceType);
                    
                    foreach(GameObject gameObject in gameObjects)
                    {
                        // Attempt to locate existing game object in the tree
                        TreeNode[] gameObjectNodesList = categoryTreeNode.Nodes.Find(gameObject.Resref, false);
                        TreeNode gameObjectTreeNode;

                        // Object doesn't exist - create it in the tree.
                        if (gameObjectNodesList.Length <= 0)
                        {
                            gameObjectTreeNode = new TreeNode();
                            categoryTreeNode.Nodes.Add(gameObjectTreeNode);
                        }
                        // Object exists - update its text
                        else
                        {
                            gameObjectTreeNode = gameObjectNodesList[0];
                        }

                        gameObjectTreeNode.Text = gameObject.Name;
                        gameObjectTreeNode.Name = gameObject.Resref;
                        gameObjectTreeNode.Tag = gameObject;
                    }
                }


                // Loop through all nodes in the tree and clear out any deleted categories or game objects.
                //foreach (TreeNode currentCategory in treeView.Nodes[0].Nodes)
                
                for(int index = treeView.Nodes[0].Nodes.Count - 1; index >= 0; index--)
                {
                    TreeNode categoryNode = treeView.Nodes[0].Nodes[index];
                    ResourceCategory resourceCategory = treeView.Nodes[0].Nodes[index].Tag as ResourceCategory;
                    if (!repo.Exists(resourceCategory))
                    {
                        //currentCategory.Remove();
                        treeView.Nodes[0].Nodes.RemoveAt(index); ;
                    }
                    else
                    {
                        // Loop through all children objects in the category and remove any deleted ones from the tree view.
                        for(int gameObjectIndex = categoryNode.Nodes.Count - 1; gameObjectIndex >= 0; gameObjectIndex--)
                        {
                            TreeNode gameObjectNode = categoryNode.Nodes[gameObjectIndex];
                            GameObject gameObject = gameObjectNode.Tag as GameObject;

                            // If the game object does not exist in the database, remove it from the tree view.
                            if (!factory.DoesObjectExistInDatabase(gameObject.Resref, GameObjectResourceType))
                            {
                                treeView.Nodes[0].Nodes[index].Nodes.RemoveAt(gameObjectIndex);
                            }
                        }
                    }
                }

            }
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
                ToolStripItem createObjectItem = contextMenuStripNodes.Items.Add("Create " + GameObjectResourceType.ToString());
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
                ToolStripItem editCopyItem = contextMenuStripNodes.Items.Add("Edit Copy");
                contextMenuStripNodes.Items.Add("-");
                ToolStripItem deleteObjectItem = contextMenuStripNodes.Items.Add("Delete " + GameObjectResourceType.ToString());

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
            NewObjectEntry newObjectEntryForm = new NewObjectEntry(GameObjectResourceType, treeView.SelectedNode.Tag as ResourceCategory);
            newObjectEntryForm.Text = "New " + GameObjectResourceType.ToString();
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
            DialogResult result = MessageBox.Show("Are you sure you want to delete this " + GameObjectResourceType.ToString().ToLower() + "?", "Delete " + GameObjectResourceType.ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            // User chose to delete the category. Remove all contained objects and delete the category.
            if (result == DialogResult.Yes)
            {
                GameObject obj = treeView.SelectedNode.Tag as GameObject;
                try
                {
                    // Remove this object from the database
                    GameObjectFactory factory = new GameObjectFactory();
                    factory.DeleteFromDatabase(obj.Resref, GameObjectResourceType);
                    
                    RefreshTreeView();
                    treeView.SelectedNode = treeView.TopNode;
                }
                catch (Exception ex)
                {
                    ErrorHelper.ShowErrorDialog("Error deleting specified " + GameObjectResourceType.ToString() + " (Method: contextMenuStripNodes_DeleteObject).\n\n", ex);
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

                //try
                {
                    // Remove the objects contained inside this category from the database
                    GameObjectFactory factory = new GameObjectFactory();
                    factory.DeleteFromDatabaseByCategory(category, GameObjectResourceType);
                    
                    // Remove the category from the database
                    using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
                    {
                        repo.DeleteResourceCategory(category);
                    }
                    RefreshTreeView();
                    treeView.SelectedNode = treeView.TopNode;
                }
                //catch (Exception ex)
                {
                    //ErrorHelper.ShowErrorDialog("Error deleting specified category (Method: contextMenuStripNodes_DeleteCategory).", ex);
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
