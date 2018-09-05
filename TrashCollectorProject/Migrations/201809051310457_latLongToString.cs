namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class latLongToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Latitude", c => c.String());
            AlterColumn("dbo.Customers", "Longitude", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Customers", "Latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
