using System.Data.Entity;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.GameObjects;
using WinterEngine.DataTransferObjects.Graphics;
using WinterEngine.DataTransferObjects.Mapping;

namespace WinterEngine.DataAccess.Contexts
{
    public class WinterContext : DbContext
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Placeable> Placeables { get; set; }
        public DbSet<Category> ResourceCategories { get; set; }
        public DbSet<GameModule> Modules { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<ItemProperty> ItemProperties { get; set; }
        public DbSet<CharacterClass> CharacterClasses { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<TileMap> TileMaps { get; set; }
        public DbSet<TileLayer> TileLayers { get; set; }
        //public DbSet<Tile> Tiles { get; set; }

        public WinterContext(string connString) : base(connString)
        {
        }

    }
}
