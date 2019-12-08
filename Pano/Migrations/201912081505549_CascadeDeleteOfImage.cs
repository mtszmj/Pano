namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeDeleteOfImage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "SceneId", "dbo.Scenes");
            AddForeignKey("dbo.Images", "SceneId", "dbo.Scenes", "SceneId", cascadeDelete: true);
            DropColumn("dbo.Images", "ImageDataId");
            DropColumn("dbo.ImageDatas", "ImageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageDatas", "ImageId", c => c.Int(nullable: false));
            AddColumn("dbo.Images", "ImageDataId", c => c.Int());
            DropForeignKey("dbo.Images", "SceneId", "dbo.Scenes");
            AddForeignKey("dbo.Images", "SceneId", "dbo.Scenes", "SceneId");
        }
    }
}
