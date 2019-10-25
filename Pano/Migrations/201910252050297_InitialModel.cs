namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HotSpots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pitch = c.Int(nullable: false),
                        Yaw = c.Int(nullable: false),
                        Text = c.String(),
                        CssClass = c.String(),
                        SceneId = c.Int(nullable: false),
                        TargetPitch = c.Int(),
                        TargetYaw = c.Int(),
                        TargetHfov = c.Int(),
                        TargetScene_SceneId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scenes", t => t.SceneId, cascadeDelete: true)
                .ForeignKey("dbo.Scenes", t => t.TargetScene_SceneId)
                .Index(t => t.SceneId)
                .Index(t => t.TargetScene_SceneId);
            
            CreateTable(
                "dbo.Scenes",
                c => new
                    {
                        SceneId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Title = c.String(),
                        Author = c.String(),
                        BasePath = c.String(),
                        AutoLoad = c.Boolean(),
                        AutoRotate = c.Single(),
                        AutoRotateInactivityDelay = c.Int(),
                        AutoRotateStopDelay = c.Int(),
                        Fallback = c.String(),
                        OrientationOnByDefault = c.Boolean(),
                        ShowZoomCtrl = c.Boolean(),
                        KeyboardZoom = c.Boolean(),
                        MouseZoom = c.Boolean(),
                        Draggable = c.Boolean(),
                        DisableKeyboardCtrl = c.Boolean(),
                        ShowFullscreenCtrl = c.Boolean(),
                        ShowControls = c.Boolean(),
                        Yaw = c.Int(),
                        Pitch = c.Int(),
                        Hfov = c.Int(),
                        MinYaw = c.Int(),
                        MaxYaw = c.Int(),
                        MinPitch = c.Int(),
                        MaxPitch = c.Int(),
                        MinHfov = c.Int(),
                        MaxHfov = c.Int(),
                        Compass = c.Boolean(),
                        NorthOffset = c.Int(),
                        Preview = c.String(),
                        PreviewTitle = c.String(),
                        PreviewAuthor = c.String(),
                        HorizonPitch = c.Int(),
                        HorizonRoll = c.Int(),
                        EscapeHTML = c.Boolean(),
                        CrossOrigin = c.String(),
                        HotSpotDebug = c.Boolean(),
                        SceneFadeDuration = c.Int(),
                        Dynamic = c.Boolean(),
                        Panorama = c.String(),
                        Haov = c.Int(),
                        Vaov = c.Int(),
                        VOffset = c.Int(),
                        IgnoreGPanoXMP = c.Boolean(),
                        Tour_TourForDbId = c.Int(),
                    })
                .PrimaryKey(t => t.SceneId)
                .ForeignKey("dbo.TourForDbs", t => t.Tour_TourForDbId)
                .Index(t => t.Tour_TourForDbId);
            
            CreateTable(
                "dbo.StringDictionaryEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        Scene_SceneId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scenes", t => t.Scene_SceneId)
                .Index(t => t.Scene_SceneId);
            
            CreateTable(
                "dbo.TourForDbs",
                c => new
                    {
                        TourForDbId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourForDbId)
                .ForeignKey("dbo.Projects", t => t.TourForDbId)
                .Index(t => t.TourForDbId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DateOfCreation = c.DateTime(nullable: false),
                        DateOfLastModification = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HotSpots", "TargetScene_SceneId", "dbo.Scenes");
            DropForeignKey("dbo.Scenes", "Tour_TourForDbId", "dbo.TourForDbs");
            DropForeignKey("dbo.TourForDbs", "TourForDbId", "dbo.Projects");
            DropForeignKey("dbo.StringDictionaryEntries", "Scene_SceneId", "dbo.Scenes");
            DropForeignKey("dbo.HotSpots", "SceneId", "dbo.Scenes");
            DropIndex("dbo.TourForDbs", new[] { "TourForDbId" });
            DropIndex("dbo.StringDictionaryEntries", new[] { "Scene_SceneId" });
            DropIndex("dbo.Scenes", new[] { "Tour_TourForDbId" });
            DropIndex("dbo.HotSpots", new[] { "TargetScene_SceneId" });
            DropIndex("dbo.HotSpots", new[] { "SceneId" });
            DropTable("dbo.Projects");
            DropTable("dbo.TourForDbs");
            DropTable("dbo.StringDictionaryEntries");
            DropTable("dbo.Scenes");
            DropTable("dbo.HotSpots");
        }
    }
}
