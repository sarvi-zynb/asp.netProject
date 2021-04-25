namespace WebAppProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductEdited1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductName", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Products", "Image", c => c.Binary(nullable: false));
            DropColumn("dbo.Products", "ProdactName");
            DropColumn("dbo.Orders", "EmploeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "EmploeeId", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "ProdactName", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Products", "Image", c => c.Binary());
            DropColumn("dbo.Products", "ProductName");
        }
    }
}
