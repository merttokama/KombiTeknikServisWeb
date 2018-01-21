namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FaultReportConfirmation",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        OperationID = c.String(maxLength: 128),
                        Approved = c.Boolean(nullable: false),
                        Message = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.OperationID)
                .Index(t => t.OperationID);
            
            CreateTable(
                "dbo.FaultReports",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(maxLength: 128),
                        Image = c.Byte(nullable: false),
                        Location = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 250),
                        Description = c.String(nullable: false, maxLength: 250),
                        FaultReportDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        FaultReportID = c.String(nullable: false, maxLength: 128),
                        FaultIsResolved = c.Boolean(nullable: false),
                        CompletionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FaultReports", t => t.FaultReportID, cascadeDelete: true)
                .Index(t => t.FaultReportID);
            
            CreateTable(
                "dbo.WorksReports",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        WorkID = c.String(nullable: false, maxLength: 128),
                        Report = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Works", t => t.WorkID, cascadeDelete: true)
                .Index(t => t.WorkID);
            
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(maxLength: 128),
                        Question1 = c.Int(nullable: false),
                        Question2 = c.Int(nullable: false),
                        Question3 = c.Int(nullable: false),
                        Question4 = c.Int(nullable: false),
                        Question5 = c.Int(nullable: false),
                        Explanation = c.String(maxLength: 250),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Surveys", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.WorksReports", "WorkID", "dbo.Works");
            DropForeignKey("dbo.Works", "FaultReportID", "dbo.FaultReports");
            DropForeignKey("dbo.FaultReports", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FaultReportConfirmation", "OperationID", "dbo.AspNetUsers");
            DropIndex("dbo.Surveys", new[] { "UserID" });
            DropIndex("dbo.WorksReports", new[] { "WorkID" });
            DropIndex("dbo.Works", new[] { "FaultReportID" });
            DropIndex("dbo.FaultReports", new[] { "UserID" });
            DropIndex("dbo.FaultReportConfirmation", new[] { "OperationID" });
            DropTable("dbo.Surveys");
            DropTable("dbo.WorksReports");
            DropTable("dbo.Works");
            DropTable("dbo.FaultReports");
            DropTable("dbo.FaultReportConfirmation");
        }
    }
}
