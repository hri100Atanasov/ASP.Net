namespace ASP.NetMVC_Tutorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMovieProperties : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Movies SET Genre = 'Horror' WHERE Id=1");
            Sql("UPDATE Movies SET Genre = 'Comedy' WHERE Id=2");
            Sql("UPDATE Movies SET Genre = 'Tragedy' WHERE Id=3");
            Sql("UPDATE Movies SET Genre = 'Action' WHERE Id=4");
        }
        
        public override void Down()
        {
        }
    }
}
