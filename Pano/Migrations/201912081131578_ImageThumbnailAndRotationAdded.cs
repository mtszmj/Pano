namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageThumbnailAndRotationAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "Thumbnail", c => c.Binary());
            AddColumn("dbo.Images", "Rotation", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Rotation");
            DropColumn("dbo.Images", "Thumbnail");
        }
    }
}
