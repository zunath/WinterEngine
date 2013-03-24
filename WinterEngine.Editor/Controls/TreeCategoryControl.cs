using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WinterEngine.DataAccess;
using WinterEngine.DataTransferObjects.Enumerations;
using WinterEngine.DataTransferObjects.EventArgsExtended;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Resources;
using WinterEngine.Editor.Forms;
using WinterEngine.Forms.Shared;
using WinterEngine.Library.Factories;
using WinterEngine.Library.Utility;

namespace WinterEngine.Editor.Controls
{
    public partial class TreeCategoryControl : UserControl
    {
        #region Constants

        // Minimum and maximum lengths category names can be.
        private const int MinCategoryNameLength = 1;
        private const int MaxCategoryNameLength = 32;

        #endregion

        #region Fields

        private GameObjectTypeEnum _resourceTypeEnum;
        private GameObjectBase _activeGameObject;
        private TreeNode _activeTreeNode;
        
        #endregion

        #region Events / Delegates

        public event EventHandler<GameObjectEventArgs> OnOpenObject;

        #endregion

        #region Properties

        [Description("The resource type to add the category to.")]
        [Browsable(true)]
        public GameObjectTypeEnum GameObjectResourceType
        {
            get { return _resourceTypeEnum; }
            set { _resourceTypeEnum = value; }
        }

        /// <summary>
        /// Gets or sets the active game object.
        /// </summary>
        public GameObjectBase ActiveGameObject
        {
            get { return _activeGameObject; }
            set 
            {
                if (!Object.ReferenceEquals(_activeTreeNode, null))
                {
                    _activeTreeNode.Tag = value;
                }
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
            try
            {
                using (CategoryRepository repo = new CategoryRepository())
                {
                    Category resourceCategory = new Category();
                    resourceCategory.VisibleName = inputText;
                    resourceCategory.GameObjectType = GameObjectResourceType;

                    repo.Add(resourceCategory);
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
                using (CategoryRepository repo = new CategoryRepository())
                {
                    Category resourceCategoryDTO = treeView.SelectedNode.Tag as Category;
                    resourceCategoryDTO.VisibleName = inputText;
                    repo.Update(resourceCategoryDTO);
                    treeView.SelectedNode.Text = resourceCategoryDTO.VisibleName;
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
        /// Determines what type of node a tree node is.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private ObjectTreeTypeEnum GetNodeType(TreeNode node)
        {
            if (node == treeView.TopNode)
            {
                return ObjectTreeTypeEnum.Root;
            }
            else if (node.Parent == treeView.TopNode)
            {
                return ObjectTreeTypeEnum.Category;
            }
            else
            {
                return ObjectTreeTypeEnum.Object;
            }
        }

        /// <summary>
        /// Adds missing categories and objects to the tree view.
        /// Removes deleted categories and objects from the tree view.
        /// </summary>
        public void RefreshTreeView()
        {
            GameObjectFactory factory = new GameObjectFactory();
            
            using (CategoryRepository repo = new CategoryRepository())
            {
                // Retrieve all resource categories
                List<Category> resourceCategories = repo.GetAllResourceCategoriesByResourceType(GameObjectResourceType);
                
                // Loop through each resource category
                foreach (Category category in resourceCategories)
                {
                    // Generate the category's key (its ID in the database)
                    string categoryKey = Convert.ToString(category.ResourceID);

                    TreeNode[] categoryNodesList = treeView.Nodes[0].Nodes.Find(categoryKey, false);
                    TreeNode categoryTreeNode;

                    // Category doesn't exist - create it in the tree
                    if (categoryNodesList.Length <= 0)
                    {
                        categoryTreeNode = new TreeNode();
                        categoryTreeNode.Name = categoryKey;
                        categoryTreeNode.Text = category.VisibleName;
                        treeView.Nodes[0].Nodes.Add(categoryTreeNode);
                    }
                    // Category exists - update its text
                    else
                    {
                        categoryTreeNode = categoryNodesList[0];
                        categoryTreeNode.Text = category.VisibleName;
                    }

                    // Update the category object located on the object's tag
                    categoryTreeNode.Tag = category;

                    // Add object nodes to the category if they do not exist already.
                    List<GameObjectBase> gameObjects = factory.GetAllFromDatabaseByResourceCategory(category, GameObjectResourceType);
                    
                    foreach(GameObjectBase gameObject in gameObjects)
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
                for(int index = treeView.Nodes[0].Nodes.Count - 1; index >= 0; index--)
                {
                    TreeNode categoryNode = treeView.Nodes[0].Nodes[index];
                    Category resourceCategory = treeView.Nodes[0].Nodes[index].Tag as Category;
                    if (!repo.Exists(resourceCategory))
                    {
                        treeView.Nodes[0].Nodes.RemoveAt(index); ;
                    }
                    else
                    {
                        // Loop through all children objects in the category and remove any deleted ones from the tree view.
                        for(int gameObjectIndex = categoryNode.Nodes.Count - 1; gameObjectIndex >= 0; gameObjectIndex--)
                        {
                            TreeNode gameObjectNode = categoryNode.Nodes[gameObjectIndex];
                            GameObjectBase gameObject = gameObjectNode.Tag as GameObjectBase;

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
        private List<Category> GetTreeNodeTagResourceDTOs(TreeNodeCollection treeNodeCollection)
        {
            List<Category> objectList = new List<Category>();

            foreach (TreeNode currentObject in treeNodeCollection)
            {
                Category tag = currentObject.Tag as Category;
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
                        GameObjectBase gameObject = gameObjectNode.Tag as GameObjectBase;
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
            ObjectTreeTypeEnum nodeType = GetNodeType(e.Node);
            // Selected node was the root node.
            if (nodeType == ObjectTreeTypeEnum.Root)
            {
            }
            // Selected node was a category
            else if (nodeType == ObjectTreeTypeEnum.Category)
            {
            }
            // Selected node was an object
            else if (nodeType == ObjectTreeTypeEnum.Object)
            {
                GameObjectEventArgs eventArgs = new GameObjectEventArgs();
                eventArgs.GameObject = e.Node.Tag as GameObjectBase;
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
                Category resourceCategory = treeView.SelectedNode.Tag as Category;
                // Add options for category context menu. 
                // Note that system categories cannot be renamed or deleted
                ToolStripItem createObjectItem = contextMenuStripNodes.Items.Add("Create " + GameObjectResourceType.ToString());

                if (!resourceCategory.IsSystemResource)
                {
                    contextMenuStripNodes.Items.Add("-");
                    ToolStripItem renameCategoryItem = contextMenuStripNodes.Items.Add("Rename");
                    ToolStripItem deleteCategoryItem = contextMenuStripNodes.Items.Add("Delete Category");

                    renameCategoryItem.Click += new EventHandler(contextMenuStripNodes_RenameCategory);
                    deleteCategoryItem.Click += new EventHandler(contextMenuStripNodes_DeleteCategory);
                }
                createObjectItem.Click += new EventHandler(contextMenuStripNodes_CreateObject);

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
            NewObjectEntry newObjectEntryForm = new NewObjectEntry(GameObjectResourceType, treeView.SelectedNode.Tag as Category);
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
            eventArgs.GameObject = treeView.SelectedNode.Tag as GameObjectBase;
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
                GameObjectBase obj = treeView.SelectedNode.Tag as GameObjectBase;
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
                Category category = treeView.SelectedNode.Tag as Category;

                try
                {
                    // Remove the objects contained inside this category from the database
                    GameObjectFactory factory = new GameObjectFactory();
                    factory.DeleteFromDatabaseByCategory(category, GameObjectResourceType);
                    
                    // Remove the category from the database
                    using (CategoryRepository repo = new CategoryRepository())
                    {
                        repo.Delete(category);
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
                    ObjectTreeTypeEnum nodeType = GetNodeType(selectedNode);
                    
                    if (nodeType == ObjectTreeTypeEnum.Root)
                    {
                        // Left empty for future use.
                    }
                    else if (nodeType == ObjectTreeTypeEnum.Category)
                    {
                        contextMenuStripNodes_DeleteCategory(this, null);
                    }
                    else if(nodeType == ObjectTreeTypeEnum.Object)
                    {
                        contextMenuStripNodes_DeleteObject(this, null);
                    }
                }
            }
        }

        #endregion

        #region Drag-Drop Event Handling

        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode treeNode = e.Item as TreeNode;

            if(GetNodeType(treeNode) == ObjectTreeTypeEnum.Object)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            Point dropPoint = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            TreeNode destinationNode = treeView.GetNodeAt(dropPoint);
            
            if (GetNodeType(destinationNode) == ObjectTreeTypeEnum.Category)
            {
                GameObjectFactory factory = new GameObjectFactory();
                Category resourceCategory = destinationNode.Tag as Category;
                TreeNode newNode = e.Data.GetData(typeof(TreeNode)) as TreeNode;

                // Update the game object's resource category ID and update the database.
                GameObjectBase gameObject = newNode.Tag as GameObjectBase;
                gameObject.ResourceCategoryID = resourceCategory.ResourceID;
                factory.UpdateInDatabase(gameObject);

                newNode.Remove();
                destinationNode.Nodes.Add(newNode);
                destinationNode.Expand();
            }

        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void treeView_DragOver(object sender, DragEventArgs e)
        {
            TreeNode node = treeView.GetNodeAt(treeView.PointToClient(new Point(e.X, e.Y)));

            if (!Object.ReferenceEquals(node.Parent, null) && node.Parent == treeView.TopNode)
            {
                treeView.SelectedNode = node;
            }
        }

        #endregion
    }
}
