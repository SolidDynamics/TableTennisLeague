using PingPongLeague.DAL;
using PingPongLeague.ServiceLayer;
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
		private MatchService _matchService = new MatchService();

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

		private Dictionary<int, string> GetRecentGames()
		{
			var matches = _matchService.GetMatches().ToList();
			matches.Reverse();
			return matches.Take(10).ToDictionary(x => x.MatchID, x => x.ToString());
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
			var playersByMonthRating = db.Players.ToList().OrderBy(x => x.GetMonthRating(DateTime.Now.Year, DateTime.Now.Month));
			var leaderboardPositions = playersByMonthRating.Select(x => new LeaderboardPosition() { Name = x.FullName, Rank = x.GetMonthRating(DateTime.Now.Year, DateTime.Now.Month), Rating = x.GetMonthRating(DateTime.Now.Year, DateTime.Now.Month), Form = x.Form });
			return leaderboardPositions.ToList();
		}
	}
}