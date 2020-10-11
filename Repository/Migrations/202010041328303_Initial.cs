namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Code = c.Int(),
                        PlaceName = c.String(),
                        State_StateId = c.Int(),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("dbo.States", t => t.State_StateId)
                .Index(t => t.State_StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                        Median = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.ScoreDetails",
                c => new
                    {
                        ScoreDetailsId = c.Int(nullable: false, identity: true),
                        AdvantageDisadvantageDecile = c.Int(),
                        DisadvantageDecile = c.Int(),
                        IndexOfEconomicResourcesScore = c.Int(),
                        IndexOfEconomicResourcesDecile = c.Int(),
                        IndexOfEducationAndOccupationScore = c.Int(),
                        IndexOfEducationAndOccupationDecile = c.Int(),
                        UsualResedantPopulation = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Score_ScoreId = c.Int(),
                    })
                .PrimaryKey(t => t.ScoreDetailsId)
                .ForeignKey("dbo.Scores", t => t.Score_ScoreId)
                .Index(t => t.Score_ScoreId);
            
            CreateTable(
                "dbo.Scores",
                c => new
                    {
                        ScoreId = c.Int(nullable: false, identity: true),
                        DisadvantageScore = c.Int(),
                        AdvantageDisadvantageScore = c.Int(),
                        Year = c.Int(nullable: false),
                        Location_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.ScoreId)
                .ForeignKey("dbo.Locations", t => t.Location_LocationId)
                .Index(t => t.Location_LocationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScoreDetails", "Score_ScoreId", "dbo.Scores");
            DropForeignKey("dbo.Scores", "Location_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Locations", "State_StateId", "dbo.States");
            DropIndex("dbo.Scores", new[] { "Location_LocationId" });
            DropIndex("dbo.ScoreDetails", new[] { "Score_ScoreId" });
            DropIndex("dbo.Locations", new[] { "State_StateId" });
            DropTable("dbo.Scores");
            DropTable("dbo.ScoreDetails");
            DropTable("dbo.States");
            DropTable("dbo.Locations");
        }
    }
}
