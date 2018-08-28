namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationUserToCustomerTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "ApplicationUserId");
            AddForeignKey("dbo.Customers", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Customers", "Email");
            DropColumn("dbo.Customers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Password", c => c.String());
            AddColumn("dbo.Customers", "Email", c => c.String());
            DropForeignKey("dbo.Customers", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "ApplicationUserId" });
            DropColumn("dbo.Customers", "ApplicationUserId");
        }
    }
}
