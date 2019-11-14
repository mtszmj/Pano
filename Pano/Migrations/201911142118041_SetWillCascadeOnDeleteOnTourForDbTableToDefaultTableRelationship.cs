namespace Pano.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetWillCascadeOnDeleteOnTourForDbTableToDefaultTableRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DefaultSceneConfigs", "Id", "dbo.TourForDbs");
            AddForeignKey("dbo.DefaultSceneConfigs", "Id", "dbo.TourForDbs", "TourForDbId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DefaultSceneConfigs", "Id", "dbo.TourForDbs");
            AddForeignKey("dbo.DefaultSceneConfigs", "Id", "dbo.TourForDbs", "TourForDbId");
        }
    }
}
