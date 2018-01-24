namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FaultReportConfirmation", "OperationID", "dbo.AspNetUsers");
            AddColumn("dbo.FaultReportConfirmation", "FaultReportID", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.FaultReportConfirmation", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.FaultReportConfirmation", "FaultReports_ID", c => c.String(maxLength: 128));
            CreateIndex("dbo.FaultReportConfirmation", "FaultReportID");
            CreateIndex("dbo.FaultReportConfirmation", "ApplicationUser_Id");
            CreateIndex("dbo.FaultReportConfirmation", "FaultReports_ID");
            AddForeignKey("dbo.FaultReportConfirmation", "FaultReports_ID", "dbo.FaultReports", "ID");
            AddForeignKey("dbo.FaultReportConfirmation", "FaultReportID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FaultReportConfirmation", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FaultReportConfirmation", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FaultReportConfirmation", "FaultReportID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FaultReportConfirmation", "FaultReports_ID", "dbo.FaultReports");
            DropIndex("dbo.FaultReportConfirmation", new[] { "FaultReports_ID" });
            DropIndex("dbo.FaultReportConfirmation", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FaultReportConfirmation", new[] { "FaultReportID" });
            DropColumn("dbo.FaultReportConfirmation", "FaultReports_ID");
            DropColumn("dbo.FaultReportConfirmation", "ApplicationUser_Id");
            DropColumn("dbo.FaultReportConfirmation", "FaultReportID");
            AddForeignKey("dbo.FaultReportConfirmation", "OperationID", "dbo.AspNetUsers", "Id");
        }
    }
}
