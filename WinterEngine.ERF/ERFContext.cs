using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Resources;

namespace WinterEngine.DataAccess.Contexts
{
    public class ERFContext : DbContext
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Placeable> Placeables { get; set; }
        public DbSet<Category> ResourceCategories { get; set; }

        public ERFContext(string connString) : base(connString)
        {
        }
    }
}
