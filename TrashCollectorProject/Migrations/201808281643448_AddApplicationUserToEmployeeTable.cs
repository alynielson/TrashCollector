namespace TrashCollectorProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationUserToEmployeeTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Employees", "ApplicationUserId");
            AddForeignKey("dbo.Employees", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Employees", "Email");
            DropColumn("dbo.Employees", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Password", c => c.String());
            AddColumn("dbo.Employees", "Email", c => c.String());
            DropForeignKey("dbo.Employees", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Employees", new[] { "ApplicationUserId" });
            DropColumn("dbo.Employees", "ApplicationUserId");
        }
    }
}
