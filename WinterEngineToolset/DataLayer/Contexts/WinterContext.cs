using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.WinterObjects;
using WinterEngine.Toolset.DataLayer.DataTransferObjects.ResourceObjects;
using WinterEngine.Toolset.DataLayer.DataTransferObjects;

namespace WinterEngine.Toolset.DataLayer.Contexts
{
    public class WinterContext : DbContext
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Placeable> Placeables { get; set; }
        public DbSet<ResourceCategory> ResourceCategories { get; set; }
        public DbSet<ModuleDetail> ModuleDetails { get; set; }

        public WinterContext(string connString) : base(connString)
        {
        }

    }
}
