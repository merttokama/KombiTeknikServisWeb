namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        FaultReportsID = c.String(nullable: false, maxLength: 128),
                        Image = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FaultReports", t => t.FaultReportsID, cascadeDelete: true)
                .Index(t => t.FaultReportsID);
            
            DropColumn("dbo.FaultReports", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FaultReports", "Image", c => c.Byte(nullable: false));
            DropForeignKey("dbo.Images", "FaultReportsID", "dbo.FaultReports");
            DropIndex("dbo.Images", new[] { "FaultReportsID" });
            DropTable("dbo.Images");
        }
    }
}
