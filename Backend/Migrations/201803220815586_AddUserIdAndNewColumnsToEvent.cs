namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdAndNewColumnsToEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "UserId", c => c.Long(nullable: false));
            AddColumn("dbo.Events", "Title", c => c.String());
            AddColumn("dbo.Events", "Description", c => c.String());
            AddColumn("dbo.Events", "UsersMax", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "UserId");
            AddForeignKey("dbo.Events", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "UserId", "dbo.Users");
            DropIndex("dbo.Events", new[] { "UserId" });
            DropColumn("dbo.Events", "UsersMax");
            DropColumn("dbo.Events", "Description");
            DropColumn("dbo.Events", "Title");
            DropColumn("dbo.Events", "UserId");
        }
    }
}
