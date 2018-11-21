namespace PingPongLeague.Migrations
{
	using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Competition",
                c => new
                    {
                        CompetitionID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Subtitle = c.String(),
                        KFactor = c.Int(),
                        MaxChallengePlaces = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.CompetitionID);
            
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        MatchID = c.Int(nullable: false, identity: true),
                        DateOfMatch = c.DateTime(nullable: false),
                        DayMatchOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MatchID);
            
            CreateTable(
                "dbo.MatchParticipation",
                c => new
                    {
                        MatchID = c.Int(nullable: false),
                        Winner = c.Boolean(nullable: false),
                        PlayerID = c.Int(nullable: false),
                        AllTimeCompetitionResult_OpeningRating = c.Int(nullable: false),
                        AllTimeCompetitionResult_TransformedRating = c.Double(nullable: false),
                        AllTimeCompetitionResult_ExpectedScore = c.Double(nullable: false),
                        AllTimeCompetitionResult_KFactor = c.Int(nullable: false),
                        AllTimeCompetitionResult_ClosingRating = c.Int(nullable: false),
                        AllTimeCompetitionResult_ActualScore = c.Int(nullable: false),
                        AllTimeCompetitionResult_OpponentRating = c.Int(nullable: false),
                        AllTimeCompetitionResult_OpponentTransformedRating = c.Double(nullable: false),
                        MonthlyCompetitionResult_StartingRank = c.Int(nullable: false),
                        MonthlyCompetitionResult_QualifiesAsLadderChallenge = c.Boolean(nullable: false),
                        MonthlyCompetitionResult_EndingRank = c.Int(nullable: false),
                        AllTimeCompetition_CompetitionID = c.Int(),
                        MonthlyCompetition_CompetitionID = c.Int(),
                    })
                .PrimaryKey(t => new { t.MatchID, t.Winner })
                .ForeignKey("dbo.Competition", t => t.AllTimeCompetition_CompetitionID)
                .ForeignKey("dbo.Match", t => t.MatchID, cascadeDelete: true)
                .ForeignKey("dbo.Competition", t => t.MonthlyCompetition_CompetitionID)
                .ForeignKey("dbo.Player", t => t.PlayerID, cascadeDelete: true)
                .Index(t => t.MatchID)
                .Index(t => t.PlayerID)
                .Index(t => t.AllTimeCompetition_CompetitionID)
                .Index(t => t.MonthlyCompetition_CompetitionID);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.PlayerID);
		}

		public override void Down()
        {
            DropForeignKey("dbo.MatchParticipation", "PlayerID", "dbo.Player");
            DropForeignKey("dbo.MatchParticipation", "MonthlyCompetition_CompetitionID", "dbo.Competition");
            DropForeignKey("dbo.MatchParticipation", "MatchID", "dbo.Match");
            DropForeignKey("dbo.MatchParticipation", "AllTimeCompetition_CompetitionID", "dbo.Competition");
            DropIndex("dbo.MatchParticipation", new[] { "MonthlyCompetition_CompetitionID" });
            DropIndex("dbo.MatchParticipation", new[] { "AllTimeCompetition_CompetitionID" });
            DropIndex("dbo.MatchParticipation", new[] { "PlayerID" });
            DropIndex("dbo.MatchParticipation", new[] { "MatchID" });
            DropTable("dbo.Player");
            DropTable("dbo.MatchParticipation");
            DropTable("dbo.Match");
            DropTable("dbo.Competition");
        }
    }
}
