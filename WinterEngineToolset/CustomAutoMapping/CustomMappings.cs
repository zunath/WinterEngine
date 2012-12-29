﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WinterEngine.Toolset.Data.Database;
using WinterEngine.Toolset.Data.DataTransferObjects;
using AutoMapper;

namespace WinterEngine.Toolset.CustomAutoMapping
{
    public static class CustomMappings
    {
        /// <summary>
        /// Handles custom mapping types by way of the AutoMapper library.
        /// </summary>
        public static void Initialize()
        {
            #region Database object -> DTO class Mapping

            Mapper.CreateMap<Area, AreaDTO>();
            Mapper.CreateMap<Creature, CreatureDTO>();
            Mapper.CreateMap<Item, ItemDTO>();
            Mapper.CreateMap<Placeable, PlaceableDTO>();
            Mapper.CreateMap<ResourceCategory, ResourceCategoryDTO>();
            Mapper.CreateMap<ResourceType, ResourceTypeDTO>();

            #endregion

            #region DTO class -> Database object Mapping

            Mapper.CreateMap<AreaDTO, Area>();
            Mapper.CreateMap<CreatureDTO, Creature>();
            Mapper.CreateMap<ItemDTO, Item>();
            Mapper.CreateMap<PlaceableDTO, Placeable>();
            Mapper.CreateMap<ResourceCategoryDTO, ResourceCategory>();
            Mapper.CreateMap<ResourceTypeDTO, ResourceType>();


            #endregion
        }
    }
}
