namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScenesToTour : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StringDictionaryEntries", "Scene_SceneId", "dbo.Scenes");
            DropForeignKey("dbo.Scenes", "Tour_TourForDbId", "dbo.TourForDbs");
            DropIndex("dbo.Scenes", new[] { "Tour_TourForDbId" });
            DropIndex("dbo.StringDictionaryEntries", new[] { "Scene_SceneId" });
            RenameColumn(table: "dbo.Scenes", name: "Tour_TourForDbId", newName: "TourId");
            AlterColumn("dbo.Scenes", "TourId", c => c.Int(nullable: false));
            CreateIndex("dbo.Scenes", "TourId");
            AddForeignKey("dbo.Scenes", "TourId", "dbo.TourForDbs", "TourForDbId", cascadeDelete: true);
            DropColumn("dbo.StringDictionaryEntries", "Scene_SceneId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StringDictionaryEntries", "Scene_SceneId", c => c.Int());
            DropForeignKey("dbo.Scenes", "TourId", "dbo.TourForDbs");
            DropIndex("dbo.Scenes", new[] { "TourId" });
            AlterColumn("dbo.Scenes", "TourId", c => c.Int());
            RenameColumn(table: "dbo.Scenes", name: "TourId", newName: "Tour_TourForDbId");
            CreateIndex("dbo.StringDictionaryEntries", "Scene_SceneId");
            CreateIndex("dbo.Scenes", "Tour_TourForDbId");
            AddForeignKey("dbo.Scenes", "Tour_TourForDbId", "dbo.TourForDbs", "TourForDbId");
            AddForeignKey("dbo.StringDictionaryEntries", "Scene_SceneId", "dbo.Scenes", "SceneId");
        }
    }
}
