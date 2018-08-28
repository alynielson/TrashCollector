namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDateTimeOnCustomer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomStartDate", c => c.DateTime());
            AlterColumn("dbo.Customers", "CustomEndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CustomEndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "CustomStartDate", c => c.DateTime(nullable: false));
        }
    }
}
