namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Technicions",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(maxLength: 128),
                        Appropriate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            AddColumn("dbo.Works", "TechnicionID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Works", "TechnicionID");
            AddForeignKey("dbo.Works", "TechnicionID", "dbo.Technicions", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Works", "TechnicionID", "dbo.Technicions");
            DropForeignKey("dbo.Technicions", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Technicions", new[] { "UserID" });
            DropIndex("dbo.Works", new[] { "TechnicionID" });
            DropColumn("dbo.Works", "TechnicionID");
            DropTable("dbo.Technicions");
        }
    }
}
