namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FaultReportConfirmation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FaultReportID = c.Int(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        Message = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FaultReports", t => t.FaultReportID, cascadeDelete: true)
                .Index(t => t.FaultReportID);
            
            CreateTable(
                "dbo.FaultReports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        LocationX = c.String(nullable: false),
                        LocationY = c.String(nullable: false),
                        Address = c.String(nullable: false, maxLength: 250),
                        Description = c.String(nullable: false, maxLength: 250),
                        FaultReportDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 25),
                        Surname = c.String(maxLength: 25),
                        RegisterDate = c.DateTime(nullable: false),
                        ActivationCode = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MessageDate = c.DateTime(nullable: false),
                        Content = c.String(nullable: false),
                        SendBy = c.String(nullable: false, maxLength: 128),
                        SentTo = c.String(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.SendBy, cascadeDelete: true)
                .Index(t => t.SendBy);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
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
            
            CreateTable(
                "dbo.Technicions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        Appropriate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FaultReportID = c.Int(nullable: false),
                        TechnicionID = c.Int(nullable: false),
                        FaultIsResolved = c.Boolean(nullable: false),
                        CompletionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FaultReports", t => t.FaultReportID, cascadeDelete: true)
                .ForeignKey("dbo.Technicions", t => t.TechnicionID, cascadeDelete: true)
                .Index(t => t.FaultReportID)
                .Index(t => t.TechnicionID);
            
            CreateTable(
                "dbo.WorksReports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WorkID = c.Int(nullable: false),
                        Report = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Works", t => t.WorkID, cascadeDelete: true)
                .Index(t => t.WorkID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FaultReportsID = c.Int(nullable: false),
                        Image = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FaultReports", t => t.FaultReportsID, cascadeDelete: true)
                .Index(t => t.FaultReportsID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 200),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Images", "FaultReportsID", "dbo.FaultReports");
            DropForeignKey("dbo.FaultReportConfirmation", "FaultReportID", "dbo.FaultReports");
            DropForeignKey("dbo.WorksReports", "WorkID", "dbo.Works");
            DropForeignKey("dbo.Works", "TechnicionID", "dbo.Technicions");
            DropForeignKey("dbo.Works", "FaultReportID", "dbo.FaultReports");
            DropForeignKey("dbo.Technicions", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Surveys", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "SendBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FaultReports", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Images", new[] { "FaultReportsID" });
            DropIndex("dbo.WorksReports", new[] { "WorkID" });
            DropIndex("dbo.Works", new[] { "TechnicionID" });
            DropIndex("dbo.Works", new[] { "FaultReportID" });
            DropIndex("dbo.Technicions", new[] { "UserID" });
            DropIndex("dbo.Surveys", new[] { "UserID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "SendBy" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.FaultReports", new[] { "UserID" });
            DropIndex("dbo.FaultReportConfirmation", new[] { "FaultReportID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Images");
            DropTable("dbo.WorksReports");
            DropTable("dbo.Works");
            DropTable("dbo.Technicions");
            DropTable("dbo.Surveys");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.FaultReports");
            DropTable("dbo.FaultReportConfirmation");
        }
    }
}
