namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FaultReports", "LocationX", c => c.String(nullable: false));
            AddColumn("dbo.FaultReports", "LocationY", c => c.String(nullable: false));
            DropColumn("dbo.FaultReports", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FaultReports", "Location", c => c.String(nullable: false));
            DropColumn("dbo.FaultReports", "LocationY");
            DropColumn("dbo.FaultReports", "LocationX");
        }
    }
}
