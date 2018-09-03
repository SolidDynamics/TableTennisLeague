using PingPongLeague.ServiceLayer;
using System;
using System.Collections.Generic;

namespace PingPongLeague.DAL
{
	public class LeagueInitializer : System.Data.Entity.DropCreateDatabaseAlways<LeagueContext>
	{
		protected override void Seed(LeagueContext context)
		{
			var players = new List<Models.Player>
			{
				new Models.Player() { PlayerID = 1, FirstName = "Andrew", LastName = "G"},
				new Models.Player() { PlayerID = 2, FirstName = "Iain", LastName = "D"},
				new Models.Player() { PlayerID = 3, FirstName = "Patrick", LastName = "H"},
				new Models.Player() { PlayerID = 4, FirstName = "Ian", LastName = "J"}

			};

			players.ForEach(p => context.Players.Add(p));
			context.SaveChanges();

			var comps = new List<Models.Competition>
			{
				new Models.Competition() { CompetitionType = Models.CompetitionType.AllTime, Name = "All time", KFactor = 32 },
				new Models.Competition() { CompetitionType = Models.CompetitionType.Monthly, Name = "Monthly", KFactor = 64 }
			};

			comps.ForEach(p => context.Competitions.Add(p));
			context.SaveChanges();

			MatchService matchService = new MatchService(context);
			//matchService.AddMatch(new DateTime(2018, 08, 31), 4, 1, MatchWinner.Player1);
			//matchService.AddMatch(new DateTime(2018, 08, 31), 3, 2, MatchWinner.Player1);
			//matchService.AddMatch(new DateTime(2018, 08, 31), 1, 2, MatchWinner.Player1);
			//matchService.AddMatch(new DateTime(2018, 08, 31), 1, 4, MatchWinner.Player1);

			matchService.AddMatch(new DateTime(2018, 09, 03), 1, 2, MatchWinner.Player1);
			matchService.AddMatch(new DateTime(2018, 09, 03), 2, 3, MatchWinner.Player1);
			matchService.AddMatch(new DateTime(2018, 09, 03), 1, 3, MatchWinner.Player1);
			matchService.AddMatch(new DateTime(2018, 09, 03), 3, 2, MatchWinner.Player1);
		}
	}
}
