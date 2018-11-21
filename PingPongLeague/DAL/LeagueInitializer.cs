using PingPongLeague.Models;
using PingPongLeague.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace PingPongLeague.DAL
{
	public class LeagueInitializer : CreateDatabaseIfNotExists<LeagueContext>
	{
		public override void InitializeDatabase(LeagueContext context)
		{
		//	context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction
			//	, string.Format("ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE", context.Database.Connection.Database));

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
				new Player() { FirstName = "Lindsay", LastName = "S" },
				new Player() { FirstName = "Mike", LastName = "D" },
				new Player() { FirstName = "Robert", LastName = "M" },
				new Player() { FirstName = "Alex", LastName = "W" },
				new Player() { FirstName = "Rachel", LastName = "F" },
				new Player() { FirstName = "Eleanor", LastName = "O" },
				new Player() { FirstName = "Tom", LastName = "P" }
			};

			players.ForEach(p => context.Players.Add(p));
			context.SaveChanges();

			var comps = new List<Competition>
			{
				new AllTimeCompetition { Name = "All time", Subtitle = "Players ranked on performances using Elo ratings", KFactor = 28 },
				new MonthlyCompetition() { Name = "Monthly", Subtitle = "Challenge players up to two places ahead to take their spot on the ladder", MaxChallengePlaces=2 }
			};

			comps.ForEach(p => context.Competitions.Add(p));
			context.SaveChanges();

			MatchService matchService = new MatchService(context);
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
			matchService.CreateMatch(new DateTime(2018, 09, 06), "Iain D", "Andrew G", MatchWinner.Player1);
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
			matchService.CreateMatch(new DateTime(2018, 10, 02), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 02), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 03), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 04), "Mike D", "Robert M", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 04), "Robert M", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 04), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 08), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 10), "Pat H", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 11), "Ian J", "Zarko V", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 11), "Zarko V", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 11), "Iain D", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 11), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 12), "Pat H", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 12), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 15), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 15), "Pat H", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 15), "Iain D", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 15), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 15), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 16), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 16), "Iain D", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 17), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 18), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 18), "Andrew G", "Zarko V", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Iain D", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Andrew G", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Andrew G", "Alex W", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Andrew G", "Zarko V", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Zarko V", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Pat H", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Caroline E", "Rachel F", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Caroline E", "Eleanor O", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Zarko V", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 19), "Pat H", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 24), "Iain D", "Mike D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 25), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 25), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 25), "Pat H", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 26), "Pat H", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 26), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 26), "Zarko V", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 26), "Zarko V", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 29), "Pat H", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 30), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 30), "Ian J", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 30), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 30), "Pat H", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 30), "Pat H", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 30), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 31), "Pat H", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 31), "Ian J", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 31), "Iain D", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 31), "Andrew G", "Tom P", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 31), "Andrew G", "Iain D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 10, 31), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 01), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 01), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 01), "Pat H", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 01), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 02), "Ian J", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 02), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 02), "Andrew G", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 02), "Pat H", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 02), "Ian J", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 02), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 02), "Ian J", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 02), "Pat H", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 02), "Ian J", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 02), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 05), "Andrew G", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 05), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 06), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 07), "Andrew G", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 07), "Ian J", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 07), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 07), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 07), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 08), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 08), "Ian J", "Zarko V", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 08), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 09), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 09), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 12), "Pat H", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 12), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 12), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 12), "Pat H", "Zarko V", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 12), "Ian J", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 12), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 13), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 13), "Ian J", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 13), "Andrew G", "Andy H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 13), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 13), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 14), "Andrew G", "Mike D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 14), "Ian J", "Mike D", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 14), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 14), "Andrew G", "Ian J", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 15), "Ian J", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 15), "Andrew G", "Pat H", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 15), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 15), "Zarko V", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 15), "Zarko V", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 16), "Ian J", "Andrew G", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 16), "Andrew G", "Sean R", MatchWinner.Player1);
			matchService.CreateMatch(new DateTime(2018, 11, 16), "Andrew G", "Ian J", MatchWinner.Player1);



		}
	}
}
