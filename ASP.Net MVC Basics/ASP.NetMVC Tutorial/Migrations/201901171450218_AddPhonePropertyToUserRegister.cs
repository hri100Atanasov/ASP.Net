namespace ASP.NetMVC_Tutorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhonePropertyToUserRegister : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Phone");
        }
    }
}
