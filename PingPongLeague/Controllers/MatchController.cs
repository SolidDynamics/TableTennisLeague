using PingPongLeague.Models;
using PingPongLeague.ServiceLayer;
using PingPongLeague.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PingPongLeague.Controllers
{
	public class MatchController : Controller
	{
		private MatchService _matchService;

		public MatchController()
		{
			_matchService = new MatchService();
		}
		// GET: Match
		public ActionResult Index()
		{
			return View(_matchService.GetMatches());
		}

		// GET: Match/Details/5
		public ActionResult Details(int id)
		{
			var match = _matchService.GetMatch(id);

			var matchVM = new MatchVM()
			{
				MatchDate = match.DateOfMatch,
				CompetitionResults = new List<CompetitionMatchResultVM>()
			};

			var matchParticipants = match.MatchParticipations;
			if (matchParticipants.Count != 2) throw new Exception($"Expected (2) participants but there is/are ({matchParticipants.Count})");
			var winnerSet = matchParticipants.Where(mp => mp.Winner);
			if (winnerSet.Count() != 1) throw new Exception($"Expected (1) winner (and 1 loser) but there is/are ({winnerSet.Count()})");

			var winner = winnerSet.Single();
			var loser = matchParticipants.Where(mp => !mp.Winner).Single();

			matchVM.WinnerName = winner.Player.FullName;
			matchVM.LoserName = loser.Player.FullName;

			var winnerCompResults = winner.CompetitionResults;
			var loserCompResults = loser.CompetitionResults;

			if (!winnerCompResults.Select(cr => cr.Competition.CompetitionID).SequenceEqual(loserCompResults.Select(cr => cr.Competition.CompetitionID)))
				throw new Exception("The two players have different competitions stored");


			foreach (AllTimeCompetitionResult compResult in winnerCompResults.OfType<AllTimeCompetitionResult>())
			{
				CompetitionMatchResultVM competitionMatchResult = new CompetitionMatchResultVM
				{
					CompetitionID = compResult.Competition.CompetitionID,
					CompetitionName = compResult.Competition.Name,
				};

				competitionMatchResult.WinnerCompResult = new CompetitionResultVM()
				{
					OpeningRating = compResult.Ratings.OpeningRating,
					TransformedRating = compResult.Ratings.TransformedRating,
					ExpectedScore = compResult.Ratings.ExpectedScore,
					ActualScore = compResult.Ratings.ActualScore,
					KFactor = compResult.Ratings.KFactor,
					ClosingRating = compResult.Ratings.ClosingRating
				};

				matchVM.CompetitionResults.Add(competitionMatchResult);
			}

			foreach (var compResult in loserCompResults.OfType<AllTimeCompetitionResult>())
			{
				matchVM.CompetitionResults
					.Where(c => c.CompetitionID == compResult.CompetitionID)
					.Single().LoserCompResult = new CompetitionResultVM()
				{
					OpeningRating = compResult.Ratings.OpeningRating,
					TransformedRating = compResult.Ratings.TransformedRating,
					ExpectedScore = compResult.Ratings.ExpectedScore,
					ActualScore = compResult.Ratings.ActualScore,
					KFactor = compResult.Ratings.KFactor,
					ClosingRating = compResult.Ratings.ClosingRating
				};
			}

			return View(matchVM);
		}

		// GET: Match/Create
		public ActionResult Create()
		{
			var matchCreateModel = new MatchCreateModel()
			{
				Players = _matchService.GetPlayers(),
				MatchDate = DateTime.Now
			};
			return View(matchCreateModel);
		}

		// POST: Match/Create
		[HttpPost]
		public ActionResult Create(MatchCreateModel match)
		{
			try
			{
				var matchID = _matchService.AddMatch(match.MatchDate, match.Winner.PlayerID, match.Loser.PlayerID, MatchWinner.Player1);
				return RedirectToAction("Details", new { Id = matchID });
			}
			catch
			{
				return View(match);
			}
		}

		// GET: Match/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Match/Edit/5
		[HttpPost]
		public ActionResult Edit(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		// GET: Match/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Match/Delete/5
		[HttpPost]
		public ActionResult Delete(int id, FormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}
	}
}
