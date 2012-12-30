﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.Database;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using WinterEngine.Toolset.DataLayer.Repositories;
using WinterEngine.Toolset.Enumerations;
using AutoMapper;

namespace WinterEngine.Toolset.Factories
{
    /// <summary>
    /// Factory method for creating game objects.
    /// </summary>
    public class WinterObjectFactory
    {
        /// <summary>
        /// Creates and returns a new object of the specified type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public WinterObjectDTO CreateObject(ResourceTypeEnum resourceType)
        {
            switch (resourceType)
            {
                case ResourceTypeEnum.Area:
                    return new AreaDTO();
                case ResourceTypeEnum.Conversation:
                    return null;
                case ResourceTypeEnum.Creature:
                    return new CreatureDTO();
                case ResourceTypeEnum.Item:
                    return new ItemDTO();
                case ResourceTypeEnum.Placeable:
                    return new PlaceableDTO();
                case ResourceTypeEnum.Script:
                    return null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns a list of all of the objects in the database for a specified resource type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public List<WinterObjectDTO> GetAllObjects(ResourceTypeEnum resourceType)
        {
            List<WinterObjectDTO> objectList = new List<WinterObjectDTO>();

            switch (resourceType)
            {
                case ResourceTypeEnum.Area:
                    {
                        using (AreaRepository repo = new AreaRepository())
                        {
                            // Handles converting all of the AreaDTO objects into WinterObjectDTOs
                            objectList = repo.GetAllAreas().ConvertAll(x => (WinterObjectDTO)x);
                        }
                    break;
                }
                case ResourceTypeEnum.Conversation:
                {
                    throw new NotImplementedException();
                }
                case ResourceTypeEnum.Creature:
                {
                    using (CreatureRepository repo = new CreatureRepository())
                    {
                        // Handles converting all of the CreatureDTO objects into WinterObjectDTOs
                        objectList = repo.GetAllCreatures().ConvertAll(x => (WinterObjectDTO)x);
                    }
                    break;
                }
                case ResourceTypeEnum.Item:
                {
                    using (ItemRepository repo = new ItemRepository())
                    {
                        // Handles converting all of the ItemDTO objects into WinterObjectDTOs
                        objectList = repo.GetAllItems().ConvertAll(x => (WinterObjectDTO)x);
                    }
                    break;
                }
                case ResourceTypeEnum.Placeable:
                {
                    using (PlaceableRepository repo = new PlaceableRepository())
                    {
                        // Handles converting all of the PlaceableDTO objects into WinterObjectDTOs
                        objectList = repo.GetAllPlaceables().ConvertAll(x => (WinterObjectDTO)x);
                    }

                    break;
                }
                case ResourceTypeEnum.Script:
                {
                    throw new NotImplementedException();
                }
            }

            return objectList;
        }

    }
}
