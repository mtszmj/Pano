namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnTitleToTourForDbsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TourForDbs", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TourForDbs", "Title");
        }
    }
}
