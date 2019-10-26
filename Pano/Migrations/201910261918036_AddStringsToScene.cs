namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStringsToScene : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StringDictionaryEntries", "SceneId", c => c.Int(nullable: false));
            CreateIndex("dbo.StringDictionaryEntries", "SceneId");
            AddForeignKey("dbo.StringDictionaryEntries", "SceneId", "dbo.Scenes", "SceneId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StringDictionaryEntries", "SceneId", "dbo.Scenes");
            DropIndex("dbo.StringDictionaryEntries", new[] { "SceneId" });
            DropColumn("dbo.StringDictionaryEntries", "SceneId");
        }
    }
}
