namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerDateScheduledPropert : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "DateScheduledThrough", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "DateScheduledThrough");
        }
    }
}
