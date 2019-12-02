namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReferenceToSceneInDefaultSceneConfig : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.DefaultSceneConfigs", "FirstSceneId");
            AddForeignKey("dbo.DefaultSceneConfigs", "FirstSceneId", "dbo.Scenes", "SceneId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefaultSceneConfigs", "FirstSceneId", "dbo.Scenes");
            DropIndex("dbo.DefaultSceneConfigs", new[] { "FirstSceneId" });
        }
    }
}
