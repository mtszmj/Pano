namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedDefault : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DefaultScenes", newName: "DefaultSceneConfigs");
            RenameColumn(table: "dbo.DefaultSceneConfigs", name: "DefaultSceneId", newName: "Id");
            RenameIndex(table: "dbo.DefaultSceneConfigs", name: "IX_DefaultSceneId", newName: "IX_Id");
            AddColumn("dbo.DefaultSceneConfigs", "FirstSceneId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DefaultSceneConfigs", "FirstSceneId");
            RenameIndex(table: "dbo.DefaultSceneConfigs", name: "IX_Id", newName: "IX_DefaultSceneId");
            RenameColumn(table: "dbo.DefaultSceneConfigs", name: "Id", newName: "DefaultSceneId");
            RenameTable(name: "dbo.DefaultSceneConfigs", newName: "DefaultScenes");
        }
    }
}
