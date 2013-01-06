using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinterEngine.Toolset.DataLayer.Contexts;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using WinterEngine.Toolset.Enumerations;
using WinterEngine.Toolset.Helpers;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class WinterObjectRepository : IRepository, IDisposable
    {

        #region Constructors

        #endregion

        #region Methods

        /// <summary>
        /// Returns all objects from the database for a particular resource type.
        /// </summary>
        /// <returns></returns>
        public List<WinterObject> GetAllObjects(ResourceTypeEnum resourceType)
        {
            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    // Retrieve areas
                    if (resourceType == ResourceTypeEnum.Area)
                    {
                        List<Area> _objectList = new List<Area>();
                        var query = from area
                                    in context.Areas
                                    select area;
                        _objectList = query.ToList<Area>();
                        return _objectList.ConvertAll(x => (WinterObject)x);
                    }

                    // Retrieve conversations
                    else if (resourceType == ResourceTypeEnum.Conversation)
                    {
                        throw new NotImplementedException();
                    }

                    // Retrieve creatures
                    else if (resourceType == ResourceTypeEnum.Creature)
                    {
                        List<Creature> _objectList = new List<Creature>();
                        var query = from creature
                                    in context.Creatures
                                    select creature;
                        _objectList = query.ToList<Creature>();
                        return _objectList.ConvertAll(x => (WinterObject)x);
                    }

                    // Retrieve items
                    else if (resourceType == ResourceTypeEnum.Item)
                    {
                        List<Item> _objectList = new List<Item>();
                        var query = from item
                                    in context.Items
                                    select item;
                        _objectList = query.ToList<Item>();
                        return _objectList.ConvertAll(x => (WinterObject)x);
                    }

                    // Retrieve placeables
                    else if (resourceType == ResourceTypeEnum.Placeable)
                    {
                        List<Placeable> _objectList = new List<Placeable>();
                        var query = from placeable
                                    in context.Placeables
                                    select placeable;
                        _objectList = query.ToList<Placeable>();
                        return _objectList.ConvertAll(x => (WinterObject)x);
                    }

                    // Retrieve scripts
                    else if (resourceType == ResourceTypeEnum.Script)
                    {
                        throw new NotImplementedException();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error retrieving all objects from database.", ex); 
            }

            return null;
        }

        /// <summary>
        /// Returns all areas that match a particular ResourceCategoryDTO's CategoryID field.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public List<WinterObject> GetAllObjectsByResourceCategory(ResourceTypeEnum resourceType, ResourceCategory resourceCategory)
        {
            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    // Retrieve areas
                    if (resourceType == ResourceTypeEnum.Area)
                    {

                        List<Area> _areaList = new List<Area>();
                        var query = from area
                                    in context.Areas
                                    where area.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                                    select area;
                        _areaList = query.ToList<Area>();
                        return _areaList.ConvertAll(x => (WinterObject)x);
                    }
                    // Retrieve conversations
                    else if (resourceType == ResourceTypeEnum.Conversation)
                    {
                        throw new NotImplementedException();
                    }
                    // Retrieve creatures
                    else if (resourceType == ResourceTypeEnum.Creature)
                    {
                        List<Creature> _creatureList = new List<Creature>();
                        var query = from creature
                                    in context.Creatures
                                    where creature.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                                    select creature;
                        _creatureList = query.ToList<Creature>();
                        return _creatureList.ConvertAll(x => (WinterObject)x);
                    }

                    // Retrieve items
                    else if (resourceType == ResourceTypeEnum.Item)
                    {
                        List<Item> _objectList = new List<Item>();
                        var query = from item
                                    in context.Items
                                    where item.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                                    select item;
                        _objectList = query.ToList<Item>();
                        return _objectList.ConvertAll(x => (WinterObject)x);
                    }

                    // Retrieve placeables
                    else if (resourceType == ResourceTypeEnum.Placeable)
                    {
                        List<Placeable> _objectList = new List<Placeable>();
                        var query = from placeable
                                    in context.Placeables
                                    where placeable.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                                    select placeable;
                        _objectList = query.ToList<Placeable>();
                        return _objectList.ConvertAll(x => (WinterObject)x);
                    }

                    // Retrieve scripts
                    else if (resourceType == ResourceTypeEnum.Script)
                    {
                        throw new NotImplementedException();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error retrieving objects by resource category from database.", ex); 
            }
            return null;
        }

        /// <summary>
        /// Returns a specified area by resref from the database
        /// </summary>
        /// <param name="resref"></param>
        /// <returns></returns>
        public WinterObject GetObjectByResref(ResourceTypeEnum resourceType, string resref)
        {
            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {

                    // Retrieve areas
                    if (resourceType == ResourceTypeEnum.Area)
                    {
                        Area retArea = new Area();
                        var query = from area
                                    in context.Areas
                                    where area.Resref.Equals(resref) // Must match the resref - primary key in database
                                    select area;
                        return query.ToList<Area>()[0];
                    }
                    // Retrieve conversations
                    else if (resourceType == ResourceTypeEnum.Conversation)
                    {
                        throw new NotImplementedException();
                    }
                    // Retrieve creatures
                    else if (resourceType == ResourceTypeEnum.Creature)
                    {
                        Creature retCreature = new Creature();
                        var query = from creature
                                    in context.Creatures
                                    where creature.Resref.Equals(resref) // Must match the resref - primary key in database
                                    select creature;
                        return query.ToList<Creature>()[0];
                    }

                    // Retrieve items
                    else if (resourceType == ResourceTypeEnum.Item)
                    {
                        Item retItem = new Item();
                        var query = from item
                                    in context.Items
                                    where item.Resref.Equals(resref) // Must match the resref - primary key in database
                                    select item;
                        return query.ToList<Item>()[0];
                    }

                    // Retrieve placeables
                    else if (resourceType == ResourceTypeEnum.Placeable)
                    {
                        Placeable retPlaceable = new Placeable();
                        var query = from placeable
                                    in context.Placeables
                                    where placeable.Resref.Equals(resref) // Must match the resref - primary key in database
                                    select placeable;
                        return query.ToList<Placeable>()[0];
                    }

                    // Retrieve scripts
                    else if (resourceType == ResourceTypeEnum.Script)
                    {
                        throw new NotImplementedException();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error retrieving specified object from database. (Resref: " + resref + ").", ex);
            }
            return null;
        }

        /// <summary>
        /// Removes all objects in a specified category.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceCategory"></param>
        public void RemoveAllObjectsInCategory(ResourceTypeEnum resourceType, ResourceCategory resourceCategory)
        {
            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    List<WinterObject> objectList = GetAllObjectsByResourceCategory(resourceType, resourceCategory);

                    foreach (WinterObject currentObject in objectList)
                    {
                        RemoveObject(resourceType, currentObject.Resref);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error deleting specified objects from database.", ex);
            }
        }

        /// <summary>
        /// Removes a specified object by its resref.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resref"></param>
        public void RemoveObject(ResourceTypeEnum resourceType, string resref)
        {
            try
            {

                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    WinterObject obj = GetObjectByResref(resourceType, resref);

                    if (resourceType == ResourceTypeEnum.Area)
                    {
                        Area area = new Area();
                        area = context.Areas.First(a => a.Resref == resref);
                        context.Areas.Remove(area);
                    }
                    else if (resourceType == ResourceTypeEnum.Conversation)
                    {
                        throw new NotImplementedException();
                    }
                    else if (resourceType == ResourceTypeEnum.Creature)
                    {
                        Creature creature = new Creature();
                        creature = context.Creatures.First(c => c.Resref == resref);
                        context.Creatures.Remove(creature);
                    }
                    else if (resourceType == ResourceTypeEnum.Item)
                    {
                        Item item = new Item();
                        item = context.Items.First(i => i.Resref == resref);
                        context.Items.Remove(item);
                    }
                    else if (resourceType == ResourceTypeEnum.Placeable)
                    {
                        Placeable placeable = new Placeable();
                        placeable = context.Placeables.First(r => r.Resref == resref);
                        context.Placeables.Remove(placeable);
                    }
                    else if (resourceType == ResourceTypeEnum.Script)
                    {
                        throw new NotImplementedException();
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error deleting specified object from database (Resref: " + resref + ").", ex);
            }
        }

        /// <summary>
        /// Adds a new object to the appropriate database table.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="winterObject"></param>
        public void AddObject(ResourceTypeEnum resourceType, int categoryID, string name, string tag, string resref)
        {
            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    switch (resourceType)
                    {
                        case ResourceTypeEnum.Area:
                        {
                            Area area = new Area();
                            area.Name = name;
                            area.Tag = tag;
                            area.Resref = resref;
                            area.ResourceCategoryID = categoryID;
                            context.Areas.Add(area);
                            context.SaveChanges();
                            break;
                        }

                        case ResourceTypeEnum.Conversation:
                        {
                            throw new NotImplementedException();
                        }

                        case ResourceTypeEnum.Creature:
                        {
                            Creature creature = new Creature();
                            creature.Name = name;
                            creature.Tag = tag;
                            creature.Resref = resref;
                            creature.ResourceCategoryID = categoryID;
                            context.Creatures.Add(creature);
                            context.SaveChanges();
                            break;
                        }

                        case ResourceTypeEnum.Item:
                        {
                            Item item = new Item();
                            item.Name = name;
                            item.Tag = tag;
                            item.Resref = resref;
                            item.ResourceCategoryID = categoryID;
                            context.Items.Add(item);
                            context.SaveChanges();
                            break;
                        }

                        case ResourceTypeEnum.Placeable:
                        {
                            Placeable placeable = new Placeable();
                            placeable.Name = name;
                            placeable.Tag = tag;
                            placeable.Resref = resref;
                            placeable.ResourceCategoryID = categoryID;
                            context.Placeables.Add(placeable);
                            context.SaveChanges();
                            break;
                        }

                        case ResourceTypeEnum.Script:
                        {
                            throw new NotImplementedException();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error adding specified object to database (Resref: " + resref + ").", ex);
            }
        }

        /// <summary>
        /// Determines whether or not an object exists in the database.
        /// Returns True if an entry is found. Returns false otherwise.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resref"></param>
        /// <returns></returns>
        public bool DoesObjectExist(ResourceTypeEnum resourceType, string resref)
        {
            try
            {
                using (WinterContext context = new WinterContext(WinterConnectionInformation.ActiveConnectionString))
                {
                    switch (resourceType)
                    {
                        // Look for an object in the database table with the resref. Then, if it's null return false. Otherwise return true.
                        case ResourceTypeEnum.Area:
                            return !Object.ReferenceEquals(context.Areas.FirstOrDefault(a => a.Resref.Equals(resref)), null);
                        case ResourceTypeEnum.Conversation:
                            throw new NotImplementedException();
                        case ResourceTypeEnum.Creature:
                            return !Object.ReferenceEquals(context.Creatures.FirstOrDefault(c => c.Resref.Equals(resref)), null);
                        case ResourceTypeEnum.Item:
                            return !Object.ReferenceEquals(context.Items.FirstOrDefault(i => i.Resref.Equals(resref)), null);
                        case ResourceTypeEnum.Placeable:
                            return !Object.ReferenceEquals(context.Placeables.FirstOrDefault(p => p.Resref.Equals(resref)), null);
                        case ResourceTypeEnum.Script:
                            throw new NotImplementedException();
                        default:
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.ShowErrorDialog("Error finding specified object in database (Resref: " + resref + ").", ex);
                return false;
            }
        }

        public void Dispose()
        {
        }

        #endregion



    }
}
