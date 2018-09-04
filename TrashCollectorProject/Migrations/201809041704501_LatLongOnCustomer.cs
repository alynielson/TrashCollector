namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatLongOnCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Customers", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Longitude");
            DropColumn("dbo.Customers", "Latitude");
        }
    }
}
