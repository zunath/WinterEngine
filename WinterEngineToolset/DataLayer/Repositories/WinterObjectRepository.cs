using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutoMapper;
using WinterEngine.Toolset.DataLayer.Database;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using WinterEngine.Toolset.Enumerations;

namespace WinterEngine.Toolset.DataLayer.Repositories
{
    /// <summary>
    /// Data access class.
    /// Handles retrieving data from the database and returning DataTransferObjects (DTOs)
    /// </summary>
    public class WinterObjectRepository : IDisposable
    {
        /// <summary>
        /// Returns all objects from the database for a particular resource type.
        /// </summary>
        /// <returns></returns>
        public List<WinterObjectDTO> GetAllObjects(ResourceTypeEnum resourceType)
        {
            try
            {
                using (WinterContext context = new WinterContext())
                {
                    // Retrieve areas
                    if (resourceType == ResourceTypeEnum.Area)
                    {
                        List<AreaDTO> _objectList = new List<AreaDTO>();
                        var query = from area
                                    in context.Areas
                                    select area;
                        _objectList = Mapper.Map(query.ToList<Area>(), _objectList);
                        return _objectList.ConvertAll(x => (WinterObjectDTO)x);
                    }

                    // Retrieve conversations
                    else if (resourceType == ResourceTypeEnum.Conversation)
                    {
                        throw new NotImplementedException();
                    }

                    // Retrieve creatures
                    else if (resourceType == ResourceTypeEnum.Creature)
                    {
                        List<CreatureDTO> _objectList = new List<CreatureDTO>();
                        var query = from creature
                                    in context.Creatures
                                    select creature;
                        _objectList = Mapper.Map(query.ToList<Creature>(), _objectList);
                        return _objectList.ConvertAll(x => (WinterObjectDTO)x);
                    }

                    // Retrieve items
                    else if (resourceType == ResourceTypeEnum.Item)
                    {
                        List<ItemDTO> _objectList = new List<ItemDTO>();
                        var query = from item
                                    in context.Items
                                    select item;
                        _objectList = Mapper.Map(query.ToList<Item>(), _objectList);
                        return _objectList.ConvertAll(x => (WinterObjectDTO)x);
                    }

                    // Retrieve placeables
                    else if (resourceType == ResourceTypeEnum.Placeable)
                    {
                        List<PlaceableDTO> _objectList = new List<PlaceableDTO>();
                        var query = from placeable
                                    in context.Placeables
                                    select placeable;
                        _objectList = Mapper.Map(query.ToList<Placeable>(), _objectList);
                        return _objectList.ConvertAll(x => (WinterObjectDTO)x);
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
                MessageBox.Show("Error retrieving all objects from database.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return null;
        }

        /// <summary>
        /// Returns all areas that match a particular ResourceCategoryDTO's CategoryID field.
        /// </summary>
        /// <param name="resourceCategory"></param>
        /// <returns></returns>
        public List<WinterObjectDTO> GetAllObjectsByResourceCategory(ResourceTypeEnum resourceType, ResourceCategoryDTO resourceCategory)
        {
            try
            {
                using (WinterContext context = new WinterContext())
                {
                    // Retrieve areas
                    if (resourceType == ResourceTypeEnum.Area)
                    {

                        List<AreaDTO> _areaList = new List<AreaDTO>();
                        var query = from area
                                    in context.Areas
                                    where area.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                                    select area;
                        _areaList = Mapper.Map(query.ToList<Area>(), _areaList);
                        return _areaList.ConvertAll(x => (WinterObjectDTO)x);
                    }
                    // Retrieve conversations
                    else if (resourceType == ResourceTypeEnum.Conversation)
                    {
                        throw new NotImplementedException();
                    }
                    // Retrieve creatures
                    else if (resourceType == ResourceTypeEnum.Creature)
                    {
                        List<CreatureDTO> _creatureList = new List<CreatureDTO>();
                        var query = from creature
                                    in context.Creatures
                                    where creature.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                                    select creature;
                        _creatureList = Mapper.Map(query.ToList<Creature>(), _creatureList);
                        return _creatureList.ConvertAll(x => (WinterObjectDTO)x);
                    }

                    // Retrieve items
                    else if (resourceType == ResourceTypeEnum.Item)
                    {
                        List<ItemDTO> _objectList = new List<ItemDTO>();
                        var query = from item
                                    in context.Items
                                    where item.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                                    select item;
                        _objectList = Mapper.Map(query.ToList<Item>(), _objectList);
                        return _objectList.ConvertAll(x => (WinterObjectDTO)x);
                    }

                    // Retrieve placeables
                    else if (resourceType == ResourceTypeEnum.Placeable)
                    {
                        List<PlaceableDTO> _objectList = new List<PlaceableDTO>();
                        var query = from placeable
                                    in context.Placeables
                                    where placeable.ResourceCategoryID.Equals(resourceCategory.ResourceCategoryID)
                                    select placeable;
                        _objectList = Mapper.Map(query.ToList<Placeable>(), _objectList);
                        return _objectList.ConvertAll(x => (WinterObjectDTO)x);
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
                MessageBox.Show("Error retrieving objects by resource category from database.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        /// <summary>
        /// Returns a specified area by resref from the database
        /// </summary>
        /// <param name="resref"></param>
        /// <returns></returns>
        public WinterObjectDTO GetObjectByResref(ResourceTypeEnum resourceType, string resref)
        {
            try
            {
                using (WinterContext context = new WinterContext())
                {

                    // Retrieve areas
                    if (resourceType == ResourceTypeEnum.Area)
                    {
                        AreaDTO retArea = new AreaDTO();
                        var query = from area
                                    in context.Areas
                                    where area.Resref.Equals(resref) // Must match the resref - primary key in database
                                    select area;
                        List<Area> resultAreas = query.ToList<Area>();
                        return Mapper.Map(resultAreas[0], retArea);
                    }
                    // Retrieve conversations
                    else if (resourceType == ResourceTypeEnum.Conversation)
                    {
                        throw new NotImplementedException();
                    }
                    // Retrieve creatures
                    else if (resourceType == ResourceTypeEnum.Creature)
                    {
                        CreatureDTO retCreature = new CreatureDTO();
                        var query = from creature
                                    in context.Creatures
                                    where creature.Resref.Equals(resref) // Must match the resref - primary key in database
                                    select creature;
                        List<Creature> resultCreatures = query.ToList<Creature>();
                        return Mapper.Map(resultCreatures[0], retCreature);
                    }

                    // Retrieve items
                    else if (resourceType == ResourceTypeEnum.Item)
                    {
                        ItemDTO retItem = new ItemDTO();
                        var query = from item
                                    in context.Items
                                    where item.Resref.Equals(resref) // Must match the resref - primary key in database
                                    select item;
                        List<Item> resultItems = query.ToList<Item>();
                        return Mapper.Map(resultItems[0], retItem);
                    }

                    // Retrieve placeables
                    else if (resourceType == ResourceTypeEnum.Placeable)
                    {
                        PlaceableDTO retPlaceable = new PlaceableDTO();
                        var query = from placeable
                                    in context.Placeables
                                    where placeable.Resref.Equals(resref) // Must match the resref - primary key in database
                                    select placeable;
                        List<Placeable> resultPlaceables = query.ToList<Placeable>();
                        return Mapper.Map(resultPlaceables[0], retPlaceable);
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
                MessageBox.Show("Error retrieving specified object from database. (Resref: " + resref + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        /// <summary>
        /// Removes all objects in a specified category.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="resourceCategory"></param>
        public void RemoveAllObjectsInCategory(ResourceTypeEnum resourceType, ResourceCategoryDTO resourceCategory)
        {
            try
            {
                using (WinterContext context = new WinterContext())
                {
                    List<WinterObjectDTO> objectList = GetAllObjectsByResourceCategory(resourceType, resourceCategory);

                    foreach (WinterObjectDTO currentObject in objectList)
                    {
                        RemoveObject(resourceType, currentObject.Resref);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting specified objects from database.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                using (WinterContext context = new WinterContext())
                {
                    WinterObjectDTO obj = GetObjectByResref(resourceType, resref);

                    if (resourceType == ResourceTypeEnum.Area)
                    {
                        Area area = new Area();
                        Mapper.Map(obj as AreaDTO, area);
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
                        Mapper.Map(obj as CreatureDTO, creature);
                        creature = context.Creatures.First(c => c.Resref == resref);  
                        context.Creatures.Remove(creature);
                    }
                    else if (resourceType == ResourceTypeEnum.Item)
                    {
                        Item item = new Item();
                        Mapper.Map(obj as ItemDTO, item);
                        item = context.Items.First(i => i.Resref == resref);    
                        context.Items.Remove(item);
                    }
                    else if (resourceType == ResourceTypeEnum.Placeable)
                    {
                        Placeable placeable = new Placeable();
                        Mapper.Map(obj as PlaceableDTO, placeable);
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
                MessageBox.Show("Error deleting specified object from database (Resref: " + resref + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                using (WinterContext context = new WinterContext())
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
                MessageBox.Show("Error adding specified object to database (Resref: " + resref + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                using (WinterContext context = new WinterContext())
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
                MessageBox.Show("Error finding specified object in database (Resref: " + resref + ").\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public void Dispose()
        {
        }

    }
}
