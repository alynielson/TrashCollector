namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pickupPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pickups", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pickups", "Price");
        }
    }
}
