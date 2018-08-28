namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PickUpEmployeeCustomerTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Address = c.String(),
                        ZipCode = c.Int(nullable: false),
                        PickupDay = c.Int(nullable: false),
                        CustomStartDate = c.DateTime(nullable: false),
                        CustomEndDate = c.DateTime(nullable: false),
                        MoneyOwed = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pickups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pickups", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Pickups", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Pickups", new[] { "EmployeeId" });
            DropIndex("dbo.Pickups", new[] { "CustomerId" });
            DropTable("dbo.Pickups");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
        }
    }
}
