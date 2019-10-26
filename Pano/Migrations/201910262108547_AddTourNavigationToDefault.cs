namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTourNavigationToDefault : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefaultScenes",
                c => new
                    {
                        DefaultSceneId = c.Int(nullable: false),
                        Hfov = c.Int(),
                        AutoLoad = c.Boolean(),
                        AutoRotate = c.Single(),
                        HotSpotDebug = c.Boolean(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.DefaultSceneId)
                .ForeignKey("dbo.TourForDbs", t => t.DefaultSceneId)
                .Index(t => t.DefaultSceneId);
            
            DropColumn("dbo.TourForDbs", "Default_Hfov");
            DropColumn("dbo.TourForDbs", "Default_AutoLoad");
            DropColumn("dbo.TourForDbs", "Default_AutoRotate");
            DropColumn("dbo.TourForDbs", "Default_HotSpotDebug");
            DropColumn("dbo.TourForDbs", "Default_Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TourForDbs", "Default_Title", c => c.String());
            AddColumn("dbo.TourForDbs", "Default_HotSpotDebug", c => c.Boolean());
            AddColumn("dbo.TourForDbs", "Default_AutoRotate", c => c.Single());
            AddColumn("dbo.TourForDbs", "Default_AutoLoad", c => c.Boolean());
            AddColumn("dbo.TourForDbs", "Default_Hfov", c => c.Int());
            DropForeignKey("dbo.DefaultScenes", "DefaultSceneId", "dbo.TourForDbs");
            DropIndex("dbo.DefaultScenes", new[] { "DefaultSceneId" });
            DropTable("dbo.DefaultScenes");
        }
    }
}
