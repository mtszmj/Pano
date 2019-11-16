namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnTargetSceneIdFromRequiredToOptionalInSceneHotSpotTable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.HotSpots", new[] { "TargetSceneId" });
            AlterColumn("dbo.HotSpots", "TargetSceneId", c => c.Int());
            CreateIndex("dbo.HotSpots", "TargetSceneId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.HotSpots", new[] { "TargetSceneId" });
            AlterColumn("dbo.HotSpots", "TargetSceneId", c => c.Int(nullable: false));
            CreateIndex("dbo.HotSpots", "TargetSceneId");
        }
    }
}
