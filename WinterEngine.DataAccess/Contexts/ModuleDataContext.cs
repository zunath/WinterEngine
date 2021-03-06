﻿using System.Data.Entity;
using WinterEngine.DataTransferObjects;

namespace WinterEngine.DataAccess.Contexts
{
    public class ModuleDataContext : DbContext
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Placeable> Placeables { get; set; }
        public DbSet<Category> ResourceCategories { get; set; }
        public DbSet<GameModule> GameModules { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<ItemProperty> ItemProperties { get; set; }
        public DbSet<CharacterClass> CharacterClasses { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Tileset> Tilesets { get; set; }
        public DbSet<Tile> Tiles { get; set; }
        public DbSet<TileCollisionBox> TileCollisionBoxes { get; set; }
        public DbSet<ContentPackage> ContentPackages { get; set; }
        public DbSet<ContentPackageResource> ContentPackageResources { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationNode> ConversationResponses { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<LocalVariable> LocalVariables { get; set; }
        public DbSet<Faction> Factions { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Animation> Animations { get; set; }
        public DbSet<LevelRequirement> LevelRequirements { get; set; }
        public DbSet<Skill> Skills { get; set; }
        
        public ModuleDataContext()
        {
            base.Database.Connection.ConnectionString = WinterConnectionInformation.ActiveConnectionString;
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
