using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.DataLayer.Database;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using WinterEngine.Toolset.DataLayer.Repositories;
using WinterEngine.Toolset.Enumerations;
using AutoMapper;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;

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
    }
}
