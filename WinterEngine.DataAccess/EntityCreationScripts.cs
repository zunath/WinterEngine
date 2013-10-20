using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.DataAccess.Factories;
using WinterEngine.DataAccess.Repositories;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.Enumerations;

namespace WinterEngine.DataAccess
{
    public static class EntityCreationScripts
    {
        public static void Initialize()
        {
            GameObjectFactory factory = new GameObjectFactory();

            #region Category Defaults
            // Add the "Uncategorized" category for each resource type.
            using (CategoryRepository repo = new CategoryRepository())
            {
                List<Category> categoryList = new List<Category>();

                categoryList.Add(new Category
                {
                    Name = "*Uncategorized",
                    GameObjectType = GameObjectTypeEnum.Area,
                    ResourceType = ResourceTypeEnum.GameObject,
                    IsSystemResource = true
                });
                categoryList.Add(new Category
                {
                    Name = "*Uncategorized",
                    GameObjectType = GameObjectTypeEnum.Conversation,
                    ResourceType = ResourceTypeEnum.GameObject,
                    IsSystemResource = true
                });
                categoryList.Add(new Category
                {
                    Name = "*Uncategorized",
                    GameObjectType = GameObjectTypeEnum.Creature,
                    ResourceType = ResourceTypeEnum.GameObject,
                    IsSystemResource = true
                });
                categoryList.Add(new Category
                {
                    Name = "*Uncategorized",
                    GameObjectType = GameObjectTypeEnum.Item,
                    ResourceType = ResourceTypeEnum.GameObject,
                    IsSystemResource = true
                });
                categoryList.Add(new Category
                {
                    Name = "*Uncategorized",
                    GameObjectType = GameObjectTypeEnum.Placeable,
                    ResourceType = ResourceTypeEnum.GameObject,
                    IsSystemResource = true
                });
                categoryList.Add(new Category
                {
                    Name = "*Uncategorized",
                    GameObjectType = GameObjectTypeEnum.Script,
                    ResourceType = ResourceTypeEnum.GameObject,
                    IsSystemResource = true
                });
                categoryList.Add(new Category
                {
                    Name = "*Uncategorized",
                    GameObjectType = GameObjectTypeEnum.Tileset,
                    ResourceType = ResourceTypeEnum.GameObject,
                    IsSystemResource = true
                });

                repo.Add(categoryList);
            }
            #endregion


            using (GenderRepository repo = new GenderRepository())
            {
                repo.Add(new Gender
                {
                    GenderType = GenderTypeEnum.Male,
                    IsSystemResource = true,
                    Name = "Male"
                });

                repo.Add(new Gender
                {
                    GenderType = GenderTypeEnum.Female,
                    IsSystemResource = true,
                    Name = "Female"
                });
            }


        }

    }
}
