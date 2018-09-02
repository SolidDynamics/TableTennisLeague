using PingPongLeague.DAL;
using PingPongLeague.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PingPongLeague.Controllers
{
	public class HomeController : Controller
	{
		private LeagueContext db = new LeagueContext();

		public ActionResult Index()
		{
			HomePageVM homePageViewModel = new HomePageVM()
			{
				AllTimeLeaderboard = GetAllTimeLeaderboard(),
				MonthLeaderboard = GetMonthLeaderboard(),
				RecentGames = GetRecentGames()
			};
			return View(homePageViewModel);
		}

		private IEnumerable<string> GetRecentGames()
		{
			return db.Matches.OrderByDescending(x => x.DateOfMatch).ThenByDescending(x => x.MatchID).Take(10).ToList().Select(x => x.ToString());
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		private IList<LeaderboardPosition> GetAllTimeLeaderboard()
		{
			var playersByAllTimeRating = db.Players.ToList().OrderByDescending(x => x.AllTimeRating);
			var leaderboardPositions = playersByAllTimeRating.Select((x, i) => new LeaderboardPosition() { Name = x.FullName, Rank = i+1, Rating = x.AllTimeRating, Form = x.Form });
			return leaderboardPositions.ToList();
		}

		private IList<LeaderboardPosition> GetMonthLeaderboard()
		{
			var playersByMonthRating = db.Players.ToList().OrderByDescending(x => x.GetMonthRating(DateTime.Now.Year, DateTime.Now.Month));
			var leaderboardPositions = playersByMonthRating.Select((x, i) => new LeaderboardPosition() { Name = x.FullName, Rank = i + 1, Rating = x.GetMonthRating(DateTime.Now.Year, DateTime.Now.Month), Form = x.Form });
			return leaderboardPositions.ToList();
		}
	}
}