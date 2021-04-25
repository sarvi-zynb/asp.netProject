namespace WebAppProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomersId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 60),
                        Gender = c.String(nullable: false, maxLength: 10),
                        Address = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 60),
                        Password = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.CustomersId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Emploees",
                c => new
                    {
                        EmploeeId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 60),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 60),
                        Password = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.EmploeeId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailsId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        UnitPrice = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderDetailsId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductsId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        ProdactName = c.String(nullable: false, maxLength: 60),
                        UnitsInStock = c.Int(nullable: false),
                        UnitPrice = c.Int(nullable: false),
                        Image = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ProductsId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrdersId = c.Int(nullable: false, identity: true),
                        CustomersId = c.Int(nullable: false),
                        EmploeeId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        ReciveDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrdersId)
                .ForeignKey("dbo.Customers", t => t.CustomersId, cascadeDelete: true)
                .Index(t => t.CustomersId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomersId", "dbo.Customers");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Emploees", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Customers", "RoleId", "dbo.Roles");
            DropIndex("dbo.Orders", new[] { "CustomersId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Emploees", new[] { "RoleId" });
            DropIndex("dbo.Customers", new[] { "RoleId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Products");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Emploees");
            DropTable("dbo.Roles");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
        }
    }
}
