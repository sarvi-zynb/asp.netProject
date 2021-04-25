namespace WebAppProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductEdited : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Image", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Image", c => c.Byte(nullable: false));
        }
    }
}
