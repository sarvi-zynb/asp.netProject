namespace WebAppProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductTableEdited : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 2000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Description");
        }
    }
}
