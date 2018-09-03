namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PaidBoolForPickup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pickups", "Paid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pickups", "Paid");
        }
    }
}
