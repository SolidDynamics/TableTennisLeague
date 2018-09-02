using PingPongLeague.DAL;
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
			return View(GetLeaderboard());
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

		private IList<LeaderboardPosition> GetLeaderboard()
		{
			var playersByAllTimeRating = db.Players.ToList().OrderByDescending(x => x.AllTimeRating);
			var leaderboardPositions = playersByAllTimeRating.Select((x, i) => new LeaderboardPosition() { Name = x.FullName, Rank = i+1, Rating = x.AllTimeRating, Form = x.Form });
			return leaderboardPositions.ToList();
		}
	}
}