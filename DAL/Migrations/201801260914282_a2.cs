namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "DosyaYolu", c => c.String());
            AddColumn("dbo.Images", "Uzanti", c => c.String());
            DropColumn("dbo.Images", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Image", c => c.Byte(nullable: false));
            DropColumn("dbo.Images", "Uzanti");
            DropColumn("dbo.Images", "DosyaYolu");
        }
    }
}
