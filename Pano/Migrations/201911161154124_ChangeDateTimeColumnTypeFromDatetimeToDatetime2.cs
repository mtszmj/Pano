namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateTimeColumnTypeFromDatetimeToDatetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Projects", "DateOfCreation", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Projects", "DateOfLastModification", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "DateOfLastModification", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Projects", "DateOfCreation", c => c.DateTime(nullable: false));
        }
    }
}
