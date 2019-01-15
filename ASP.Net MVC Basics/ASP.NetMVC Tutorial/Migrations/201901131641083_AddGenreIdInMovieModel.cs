namespace ASP.NetMVC_Tutorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreIdInMovieModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "GenreId", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "GenreId");
        }
    }
}
