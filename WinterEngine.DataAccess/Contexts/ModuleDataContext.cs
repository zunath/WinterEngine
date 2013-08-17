using System.Data.Entity;
using WinterEngine.DataTransferObjects;
using WinterEngine.DataTransferObjects.BusinessObjects;
using WinterEngine.DataTransferObjects.GameObjects;

namespace WinterEngine.DataAccess.Contexts
{
    public class ModuleDataContext : DbContext
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
        public DbSet<Tile> Tiles { get; set; }
        public DbSet<ContentPackage> ContentPackages { get; set; }
        public DbSet<ContentPackageResource> ContentPackageResources { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<LocalVariable> LocalVariables { get; set; }

        public ModuleDataContext(string connString) : base(connString)
        {
        }
    }
}
