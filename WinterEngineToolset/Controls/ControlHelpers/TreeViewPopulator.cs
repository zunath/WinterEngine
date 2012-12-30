using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DejaVu;
using WinterEngine.Toolset.DataLayer.DataTransferObjects;
using WinterEngine.Toolset.DataLayer.Repositories;
using WinterEngine.Toolset.Enumerations;
using System.Windows.Forms;

namespace WinterEngine.Toolset.Controls.ControlHelpers
{
    /// <summary>
    /// Helper class used to populating categories and objects in tree view controls.
    /// </summary>
    public class TreeViewPopulator
    {
        /// <summary>
        /// Populates a tree view with categories from the database.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="resourceType"></param>
        /// <param name="generateUncategorizedCategory"></param>
        public void PopulateTreeViewCategories(ref TreeView treeView, ResourceTypeEnum resourceType)
        {
            UndoRedoManager.StartInvisible("TreeView Category Population");

            // Retrieve list of ResourceCategoryDTO from the database by way of the data access layer and repositories.
            List<ResourceCategoryDTO> categoryList = new List<ResourceCategoryDTO>();
            using (ResourceCategoryRepository repo = new ResourceCategoryRepository())
            {
                categoryList = repo.GetAllResourceCategoriesByResourceType(resourceType);
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
        /// Note that you must populate tree view categories first or all of these objects
        /// will fall under the root.
        /// </summary>
        public void PopulateAreaTreeViewObjects(ref TreeView treeView, ResourceTypeEnum resourceType)
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
        /// Clears out all nodes on the treeView and repopulates it based on data from the database.
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="resourceType"></param>
        public void RepopulateTreeView(ref TreeView treeView, ResourceTypeEnum resourceType)
        {
            treeView.Nodes[0].Nodes.Clear();
            PopulateTreeViewCategories(ref treeView, resourceType);
            PopulateAreaTreeViewObjects(ref treeView, resourceType);
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
    }
}
