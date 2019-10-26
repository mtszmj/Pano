namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInheritanceToDefault : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Scenes", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.TourForDbs", "Default_SceneId", c => c.Int());
            CreateIndex("dbo.TourForDbs", "Default_SceneId");
            AddForeignKey("dbo.TourForDbs", "Default_SceneId", "dbo.Scenes", "SceneId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourForDbs", "Default_SceneId", "dbo.Scenes");
            DropIndex("dbo.TourForDbs", new[] { "Default_SceneId" });
            DropColumn("dbo.TourForDbs", "Default_SceneId");
            DropColumn("dbo.Scenes", "Discriminator");
        }
    }
}
