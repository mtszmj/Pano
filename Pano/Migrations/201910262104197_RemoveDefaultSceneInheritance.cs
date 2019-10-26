namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDefaultSceneInheritance : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TourForDbs", "Default_SceneId", "dbo.Scenes");
            DropIndex("dbo.TourForDbs", new[] { "Default_SceneId" });
            AddColumn("dbo.TourForDbs", "Default_Hfov", c => c.Int());
            AddColumn("dbo.TourForDbs", "Default_AutoLoad", c => c.Boolean());
            AddColumn("dbo.TourForDbs", "Default_AutoRotate", c => c.Single());
            AddColumn("dbo.TourForDbs", "Default_HotSpotDebug", c => c.Boolean());
            AddColumn("dbo.TourForDbs", "Default_Title", c => c.String());
            DropColumn("dbo.Scenes", "Discriminator");
            DropColumn("dbo.TourForDbs", "Default_SceneId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TourForDbs", "Default_SceneId", c => c.Int());
            AddColumn("dbo.Scenes", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.TourForDbs", "Default_Title");
            DropColumn("dbo.TourForDbs", "Default_HotSpotDebug");
            DropColumn("dbo.TourForDbs", "Default_AutoRotate");
            DropColumn("dbo.TourForDbs", "Default_AutoLoad");
            DropColumn("dbo.TourForDbs", "Default_Hfov");
            CreateIndex("dbo.TourForDbs", "Default_SceneId");
            AddForeignKey("dbo.TourForDbs", "Default_SceneId", "dbo.Scenes", "SceneId");
        }
    }
}
