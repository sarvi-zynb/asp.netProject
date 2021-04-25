namespace WebAppProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderTableEdited : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "OrdersId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "OrdersId");
            AddForeignKey("dbo.OrderDetails", "OrdersId", "dbo.Orders", "OrdersId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrdersId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrdersId" });
            DropColumn("dbo.OrderDetails", "OrdersId");
        }
    }
}
