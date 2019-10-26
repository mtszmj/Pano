namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHotSpots : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.HotSpots", new[] { "TargetScene_SceneId" });
            RenameColumn(table: "dbo.HotSpots", name: "TargetScene_SceneId", newName: "TargetSceneId");
            AlterColumn("dbo.HotSpots", "TargetSceneId", c => c.Int(nullable: false));
            CreateIndex("dbo.HotSpots", "TargetSceneId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.HotSpots", new[] { "TargetSceneId" });
            AlterColumn("dbo.HotSpots", "TargetSceneId", c => c.Int());
            RenameColumn(table: "dbo.HotSpots", name: "TargetSceneId", newName: "TargetScene_SceneId");
            CreateIndex("dbo.HotSpots", "TargetScene_SceneId");
        }
    }
}
