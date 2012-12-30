using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinterEngine.Toolset.DataLayer.Database;
using WinterEngine.Toolset.DataLayer.DataTransferObjects;
using AutoMapper;
using DejaVu;
using WinterEngine.Toolset.Controls.ControlHelpers;
using WinterEngine.Toolset.DataLayer.Repositories;
using WinterEngine.Toolset.Enumerations;

namespace WinterEngine.Toolset.Controls.WinterEngineControls
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

        #endregion

        #region Properties

        [Description("The resource type to add the category to.")]
        [Browsable(true)]
        public ResourceTypeEnum WinterObjectResourceType
        {
            get { return _resourceTypeEnum; }
            set { _resourceTypeEnum = value; }
        }

        #endregion

        #region Delegates and Events

        #endregion

        #region Constructors

        public TreeCategoryControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

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

        public void SuccessMethod(string inputText)
        {
            bool success;
            UndoRedoManager.Start("Add Category: " + inputText);

            using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
            {
                ResourceCategoryDTO resourceCategoryDTO = new ResourceCategoryDTO();
                resourceCategoryDTO.ResourceName = inputText;
                resourceCategoryDTO.ResourceTypeID = (int)WinterObjectResourceType;

                success = repo.AddResourceCategory(resourceCategoryDTO);
                UndoRedoManager.Commit();

                RepopulateTreeView();
                
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
            InputMessageBox inputBox = new InputMessageBox("Enter the category's name.", "New Category", MinCategoryNameLength, MaxCategoryNameLength, ValidationMethod, SuccessMethod, "Category Name");
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
            UndoRedoManager.StartInvisible("TreeView Category Population");

            // Retrieve list of ResourceCategoryDTO from the database by way of the data access layer and repositories.
            List<ResourceCategoryDTO> categoryList = new List<ResourceCategoryDTO>();
            using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
            {
                categoryList = repo.GetAllResourceCategoriesByResourceType(WinterObjectResourceType);
            }

            foreach (ResourceCategoryDTO category in categoryList)
            {
                TreeNode treeNode = new TreeNode(category.ResourceName);
                treeNode.Name = "" + category.ResourceCategoryID;
                treeNode.Tag = category;
                treeView.Nodes[0].Nodes.Add(treeNode);
            }

            treeView.Sort();
            treeView.ExpandAll();

            UndoRedoManager.Commit();
        }

        /// <summary>
        /// Populates the tree view with area objects.
        /// Note that you must populate tree view categories first or none of these will be added.
        /// </summary>
        public void PopulateAreaTreeViewObjects()
        {
            UndoRedoManager.StartInvisible("TreeView object population");
            List<AreaDTO> areaList = new List<AreaDTO>();
            TreeNodeCollection nodeCollection = treeView.Nodes[0].Nodes;
            // Get list of DTOs from the tag of tree nodes
            List<ResourceCategoryDTO> resourceDTOList = GetTreeNodeTagResourceDTOs(nodeCollection);

            using (AreaRepository repo = new AreaRepository())
            {
                areaList = repo.GetAllAreas();
            }

            foreach (AreaDTO area in areaList)
            {
                TreeNode treeNode = new TreeNode(area.Name);
                treeNode.Tag = area;

                // Find the first category that matches this area's category ID.
                ResourceCategoryDTO category = resourceDTOList.FirstOrDefault(val => val.ResourceCategoryID == area.ResourceCategoryID);

                // Unable to find category. Move this object to the first category on the list.
                if (category == null)
                {
                    // If there are no categories at all, we won't load the area.
                    if (treeView.Nodes[0].Nodes[0] != null)
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

            UndoRedoManager.Commit();
        }


        /// <summary>
        /// Populates the tree view with creature objects.
        /// Note that you must populate tree view categories first or none of these will be added.
        /// </summary>
        public void PopulateCreatureTreeViewObjects()
        {
            UndoRedoManager.StartInvisible("TreeView object population");
            List<CreatureDTO> creatureList = new List<CreatureDTO>();
            TreeNodeCollection nodeCollection = treeView.Nodes[0].Nodes;
            // Get list of DTOs from the tag of tree nodes
            List<ResourceCategoryDTO> resourceDTOList = GetTreeNodeTagResourceDTOs(nodeCollection);

            using (CreatureRepository repo = new CreatureRepository())
            {
                creatureList = repo.GetAllCreatures();
            }

            foreach (CreatureDTO creature in creatureList)
            {
                TreeNode treeNode = new TreeNode(creature.Name);
                treeNode.Tag = creature;

                // Find the first category that matches this creature's category ID.
                ResourceCategoryDTO category = resourceDTOList.FirstOrDefault(val => val.ResourceCategoryID == creature.ResourceCategoryID);

                // Unable to find category. Move this object to the first category on the list.
                if (category == null)
                {
                    // If there are no categories at all, we won't load the creature.
                    if (treeView.Nodes[0].Nodes[0] != null)
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

            UndoRedoManager.Commit();
        }


        /// <summary>
        /// Populates the tree view with item objects.
        /// Note that you must populate tree view categories first or none of these will be added.
        /// </summary>
        public void PopulateItemTreeViewObjects()
        {
            UndoRedoManager.StartInvisible("TreeView object population");
            List<ItemDTO> itemList = new List<ItemDTO>();
            TreeNodeCollection nodeCollection = treeView.Nodes[0].Nodes;
            // Get list of DTOs from the tag of tree nodes
            List<ResourceCategoryDTO> resourceDTOList = GetTreeNodeTagResourceDTOs(nodeCollection);

            using (ItemRepository repo = new ItemRepository())
            {
                itemList = repo.GetAllItems();
            }

            foreach (ItemDTO item in itemList)
            {
                TreeNode treeNode = new TreeNode(item.Name);
                treeNode.Tag = item;

                // Find the first category that matches this creature's category ID.
                ResourceCategoryDTO category = resourceDTOList.FirstOrDefault(val => val.ResourceCategoryID == item.ResourceCategoryID);

                // Unable to find category. Move this object to the first category on the list.
                if (category == null)
                {
                    // If there are no categories at all, we won't load the item.
                    if (treeView.Nodes[0].Nodes[0] != null)
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

            UndoRedoManager.Commit();
        }


        /// <summary>
        /// Populates the tree view with placeable objects.
        /// Note that you must populate tree view categories first or none of these will be added.
        /// </summary>
        public void PopulatePlaceableTreeViewObjects()
        {
            UndoRedoManager.StartInvisible("TreeView object population");
            List<PlaceableDTO> placeableList = new List<PlaceableDTO>();
            TreeNodeCollection nodeCollection = treeView.Nodes[0].Nodes;
            // Get list of DTOs from the tag of tree nodes
            List<ResourceCategoryDTO> resourceDTOList = GetTreeNodeTagResourceDTOs(nodeCollection);

            using (PlaceableRepository repo = new PlaceableRepository())
            {
                placeableList = repo.GetAllPlaceables();
            }

            foreach (PlaceableDTO placeable in placeableList)
            {
                TreeNode treeNode = new TreeNode(placeable.Name);
                treeNode.Tag = placeable;

                // Find the first category that matches this creature's category ID.
                ResourceCategoryDTO category = resourceDTOList.FirstOrDefault(val => val.ResourceCategoryID == placeable.ResourceCategoryID);

                // Unable to find category. Move this object to the first category on the list.
                if (category == null)
                {
                    // If there are no categories at all, we won't load the placeable.
                    if (treeView.Nodes[0].Nodes[0] != null)
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

            UndoRedoManager.Commit();
        }

        /// <summary>
        /// Clears out all nodes on the treeView and repopulates it based on data from the database.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="resourceType"></param>
        public void RepopulateTreeView()
        {
            treeView.Nodes[0].Nodes.Clear();
            PopulateTreeViewCategories();

            switch (WinterObjectResourceType)
            {
                case ResourceTypeEnum.Area:
                    PopulateAreaTreeViewObjects();
                    break;
                case ResourceTypeEnum.Conversation:
                    break;
                case ResourceTypeEnum.Creature:
                    PopulateCreatureTreeViewObjects();
                    break;
                case ResourceTypeEnum.Item:
                    PopulateItemTreeViewObjects();
                    break;
                case ResourceTypeEnum.Placeable:
                    PopulatePlaceableTreeViewObjects();
                    break;
                case ResourceTypeEnum.Script:
                    break;
            }

        }

        /// <summary>
        /// Retrieves a list of tags from a tree node collection
        /// </summary>
        /// <param name="treeNodeCollection"></param>
        /// <returns></returns>
        private List<ResourceCategoryDTO> GetTreeNodeTagResourceDTOs(TreeNodeCollection treeNodeCollection)
        {
            List<ResourceCategoryDTO> objectList = new List<ResourceCategoryDTO>();

            foreach (TreeNode currentObject in treeNodeCollection)
            {
                ResourceCategoryDTO tag = currentObject.Tag as ResourceCategoryDTO;
                objectList.Add(tag);
            }

            return objectList;
        }

        #endregion

        #region Tree view events

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Highlight the selected node on all clicks.
            treeView.SelectedNode = e.Node;
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
            // No context menu for the root node at this time.
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
                ToolStripItem deleteCategoryItem = contextMenuStripNodes.Items.Add("Delete Category");

                // Add events to each item click event
                createObjectItem.Click += new EventHandler(contextMenuStripNodes_CreateObject);
                deleteCategoryItem.Click += new EventHandler(contextMenuStripNodes_DeleteCategory);

            }
            // Otherwise, an area node was selected.
            else
            {
                ToolStripItem deleteObjectItem = contextMenuStripNodes.Items.Add("Delete " + WinterObjectResourceType.ToString());
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
            MessageBox.Show("Create object");
        }

        /// <summary>
        /// Event handler for deleting an object from context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripNodes_DeleteObject(object sender, EventArgs e)
        {
            MessageBox.Show("Delete object");
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
        /// Event handler for deleting a category from context menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripNodes_DeleteCategory(object sender, EventArgs e)
        {
            MessageBox.Show("Delete category");
        }

        #endregion
    }
}
