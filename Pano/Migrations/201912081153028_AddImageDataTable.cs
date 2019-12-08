namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageDataTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageDatas",
                c => new
                    {
                        ImageDataId = c.Int(nullable: false),
                        Data = c.Binary(),
                        ImageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageDataId)
                .ForeignKey("dbo.Images", t => t.ImageDataId, cascadeDelete: true)
                .Index(t => t.ImageDataId);
            
            AddColumn("dbo.Images", "ImageDataId", c => c.Int());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageDatas", "ImageDataId", "dbo.Images");
            DropIndex("dbo.ImageDatas", new[] { "ImageDataId" });
            DropColumn("dbo.Images", "ImageDataId");
            DropTable("dbo.ImageDatas");
        }
    }
}
