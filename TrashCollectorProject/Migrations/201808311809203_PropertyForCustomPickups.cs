namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyForCustomPickups : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pickups", "IsOneTime", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pickups", "IsOneTime");
        }
    }
}
