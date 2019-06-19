namespace BestSellingBooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Author", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Author", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Author", "LastName", c => c.String());
            AlterColumn("dbo.Author", "FirstName", c => c.String());
        }
    }
}
