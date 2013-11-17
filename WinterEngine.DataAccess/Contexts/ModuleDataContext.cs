using System.Data.Entity;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess.Contexts
{
    public class ModuleDataContext : DbContext
    {
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Creature> Creatures { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Placeable> Placeables { get; set; }
        public virtual DbSet<Category> ResourceCategories { get; set; }
        public virtual DbSet<GameModule> GameModules { get; set; }
        public virtual DbSet<Race> Races { get; set; }
        public virtual DbSet<ItemProperty> ItemProperties { get; set; }
        public virtual DbSet<CharacterClass> CharacterClasses { get; set; }
        public virtual DbSet<Ability> Abilities { get; set; }
        public virtual DbSet<ItemType> ItemTypes { get; set; }
        public virtual DbSet<Tileset> Tilesets { get; set; }
        public virtual DbSet<Tile> Tiles { get; set; }
        public virtual DbSet<TileCollisionBox> TileCollisionBoxes { get; set; }
        public virtual DbSet<ContentPackage> ContentPackages { get; set; }
        public virtual DbSet<ContentPackageResource> ContentPackageResources { get; set; }
        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<Script> Scripts { get; set; }
        public virtual DbSet<LocalVariable> LocalVariables { get; set; }
        public virtual DbSet<Faction> Factions { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        
        public ModuleDataContext()
        {
        }

        public ModuleDataContext(string connString) : base(connString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModuleDataContext, ModuleDataContextMigrationConfiguration>());
        }
    }
}
