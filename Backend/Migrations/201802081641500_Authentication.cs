namespace Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Authentication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Value = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GroupClaims",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GroupId = c.Long(),
                        ClaimId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Claims", t => t.ClaimId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId)
                .Index(t => t.ClaimId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(),
                        GroupId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupClaims", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Employees", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupClaims", "ClaimId", "dbo.Claims");
            DropIndex("dbo.Employees", new[] { "GroupId" });
            DropIndex("dbo.GroupClaims", new[] { "ClaimId" });
            DropIndex("dbo.GroupClaims", new[] { "GroupId" });
            DropTable("dbo.Employees");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupClaims");
            DropTable("dbo.Claims");
        }
    }
}
