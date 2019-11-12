namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagesColumnToScenesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "SceneId", c => c.Int());
            CreateIndex("dbo.Images", "SceneId");
            AddForeignKey("dbo.Images", "SceneId", "dbo.Scenes", "SceneId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "SceneId", "dbo.Scenes");
            DropIndex("dbo.Images", new[] { "SceneId" });
            DropColumn("dbo.Images", "SceneId");
        }
    }
}
