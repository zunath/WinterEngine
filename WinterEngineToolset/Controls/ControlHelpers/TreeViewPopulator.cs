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
        /// Populates the tree view with area categories.
        /// </summary>
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

            using (AreaRepository repo = new AreaRepository())
            {
                areaList = repo.GetAllAreas();
            }

            // Attempt to locate category for each object
            foreach (AreaDTO area in areaList)
            {
            }

            UndoRedoManager.Commit();
        }
    }
}
