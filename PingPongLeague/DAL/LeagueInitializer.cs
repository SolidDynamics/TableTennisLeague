using PingPongLeague.Models;
using PingPongLeague.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace PingPongLeague.DAL
{
	public class LeagueInitializer : System.Data.Entity.DropCreateDatabaseAlways<LeagueContext>
	{
		public override void InitializeDatabase(LeagueContext context)
		{
			context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
				, string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

			base.InitializeDatabase(context);
		}

		protected override void Seed(LeagueContext context)
		{
			var players = new List<Player>
			{
				new Player() { FirstName = "Andrew", LastName = "G"},
				new Player() { FirstName = "Iain", LastName = "D"},
				new Player() { FirstName = "Pat", LastName = "H"},
				new Player() { FirstName = "Ian", LastName = "J"},
				new Player() { FirstName = "Sean", LastName = "R" },
				new Player() { FirstName = "Andy", LastName = "H" },
				new Player() { FirstName = "Zarko", LastName = "V" },
				new Player() { FirstName = "Caroline", LastName = "E" },
				new Player() { FirstName = "Lindsay", LastName = "S" }
			};

			players.ForEach(p => context.Players.Add(p));
			context.SaveChanges();

			var comps = new List<Competition>
			{
				new AllTimeCompetition { Name = "All time", Subtitle = "Players ranked on performances using Elo ratings", KFactor = 32 },
				new MonthlyCompetition() { Name = "Monthly", Subtitle = "Challenge players up to two places ahead to take their spot on the ladder", MaxChallengePlaces=2 }
			};

			comps.ForEach(p => context.Competitions.Add(p));
			context.SaveChanges();

			MatchService matchService = new MatchService(context);
			//matchService.AddMatch(new DateTime(2018, 08, 31), 4, 1, MatchWinner.Player1);
			//matchService.AddMatch(new DateTime(2018, 08, 31), 3, 2, MatchWinner.Player1);
			//matchService.AddMatch(new DateTime(2018, 08, 31), 1, 2, MatchWinner.Player1);
			//matchService.AddMatch(new DateTime(2018, 08, 31), 1, 4, MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 03), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 03), "Iain D", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 03), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 03), "Pat H", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 04), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 04), "Andrew G", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 04), "Iain D", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 04), "Ian J", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 04), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 04), "Iain D", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 04), "Ian J", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 04), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 05), "Pat H", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 05), "Pat H", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 05), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 05), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 05), "Iain D", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 05), "Ian J", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 05), "Ian J", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 05), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 05), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 06), "Iain D", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 06), "Ian J", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 06), "Andy H", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 06), "Andrew G", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 06), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 06), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 06), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 06), "Andy H", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 06), "Ian J", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 07), "Iain D", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 07), "Andrew G", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 07), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 07), "Zarko V", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 10), "Ian J", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 10), "Andrew G", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 10), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 10), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 11), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 11), "Andrew G", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 11), "Ian J", "Caroline E", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 11), "Ian J", "Zarko V", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 11), "Iain D", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 11), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 11), "Iain D", "Caroline E", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 12), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 12), "Andrew G", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 12), "Ian J", "Caroline E", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 12), "Iain D", "Caroline E", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 12), "Pat H", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 12), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 12), "Andrew G", "Caroline E", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 12), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 12), "Zarko V", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 13), "Pat H", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 13), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 13), "Andy H", "Caroline E", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 13), "Andrew G", "Caroline E", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 13), "Caroline E", "Lindsay S", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 13), "Ian J", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 13), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 24), "Andrew G", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 25), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 25), "Pat H", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 25), "Iain D", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 26), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 26), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 27), "Andrew G", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 27), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 27), "Ian J", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 09, 27), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 01), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 01), "Ian J", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 01), "Iain D", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 01), "Andrew G", "Andy H", MatchWinner.Player1);




		}
	}
}
