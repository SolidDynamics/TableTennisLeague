using PingPongLeague.Models;
using PingPongLeague.ServiceLayer;
using PingPongLeague.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using EloRating;

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
				MatchDate = match.DateOfMatch
			};

			var matchParticipants = match.MatchParticipations;
			if (matchParticipants.Count != 2) throw new Exception($"Expected (2) participants but there is/are ({matchParticipants.Count})");
			var winnerSet = matchParticipants.Where(mp => mp.Winner);
			if (winnerSet.Count() != 1) throw new Exception($"Expected (1) winner (and 1 loser) but there is/are ({winnerSet.Count()})");

			var winner = winnerSet.Single();
			var loser = matchParticipants.Where(mp => !mp.Winner).Single();

			matchVM.WinnerName = winner.Player.FullName;
			matchVM.LoserName = loser.Player.FullName;


			matchVM.AllTimeMatchResult = new EloMatchResultVM()
			{
				CompetitionID = winner.AllTimeCompetition.CompetitionID,
				CompetitionName = winner.AllTimeCompetition.Name,
				WinnerResults = GetEloPlayerMatchResultVM(winner.AllTimeCompetitionResult),
				LoserResults = GetEloPlayerMatchResultVM(loser.AllTimeCompetitionResult)
			};

			matchVM.MonthlyMatchResult = new LadderMatchResultVM()
			{
				CompetitionID = winner.MonthlyCompetition.CompetitionID,
				CompetitionName = winner.MonthlyCompetition.Name,
				WinnerResults = GetLadderResultsFromCompResults(winner.MonthlyCompetitionResult),
				LoserResults = GetLadderResultsFromCompResults(loser.MonthlyCompetitionResult)
			};

			return View(matchVM);
		}

		private EloPlayerMatchResultVM GetEloPlayerMatchResultVM(EloResult eloResult)
		{
			return new EloPlayerMatchResultVM()
			{
				OpeningRating = eloResult.OpeningRating,
				TransformedRating = eloResult.TransformedRating,
				ExpectedScore = eloResult.ExpectedScore,
				ActualScore = eloResult.ActualScore,
				KFactor = eloResult.KFactor,
				ClosingRating = eloResult.ClosingRating
			};
		}

		private LadderPlayerMatchResultVM GetLadderResultsFromCompResults(LadderResult ladderResult)
		{
			return new LadderPlayerMatchResultVM()
			{
				OpeningRank = ladderResult.StartingRank,
				ClosingRank = ladderResult.EndingRank,
				MatchQualifiedForLadder = ladderResult.QualifiesAsLadderChallenge
			};
		}

		// GET: Match/Create
		public ActionResult Create()
		{

			ViewBag.Players = _matchService.GetPlayers();
			var matchCreateModel = new MatchCreateModel()
			{
				MatchDate = DateTime.Now
			};
			return View(matchCreateModel);
		}

		// POST: Match/Create
		[HttpPost]
		public ActionResult Create(MatchCreateModel match)
		{

			if (ModelState.IsValid)
			{
				try
				{
					var matchID = _matchService.CreateMatch(match.MatchDate, match.Winner, match.Loser, MatchWinner.Player1);
					return RedirectToAction("Details", new { Id = matchID });
				}
				catch (Exception e)
				{
					ModelState.AddModelError(string.Empty, e.Message);
				}
			}

			ViewBag.Players = _matchService.GetPlayers();
			return View(match);
		}

		public ActionResult OptimiseKFactor()
		{
			var eloFixtures = new List<Contest<Player>>();

			foreach (var match in _matchService.GetMatches())
			{
				var matchParticipationsList = match.MatchParticipations.ToList();
				var player1mp = matchParticipationsList[0];
				MatchParticipation player2mp = matchParticipationsList[1];

				eloFixtures.Add(new Contest<Player>
				{
					Player1 = player1mp.Player,
					Player2 = player2mp.Player,
					Result = player1mp.Winner ? ContestResult.Player1Won : ContestResult.Player2Won
				});
			}

			KFactorCalculator<Player> calculator = new KFactorCalculator<Player>(eloFixtures, 25);

			return View(calculator.GetResults());
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

		public ActionResult Ladder(int Year, int Month)
		{
			var chartSeriesList = new List<ChartSeries>();

			foreach (var playerWithResults in _matchService.GetMatchParticipations()
				.GroupBy(cr => cr.Player, cr => cr, (key, g) => new { Player = key, Results = g }).ToList())
			{
				ChartSeries chartSeries = new ChartSeries(playerWithResults.Player.FullName)
				{
					DataPoints = new List<DataPoint>()
				};
				foreach (var result in playerWithResults.Results)
				{
					chartSeries.DataPoints.Add(
						new DataPoint(
							result.Match.DateOfMatch.ToString("dd/MM"),
							Convert.ToDouble(result.MonthlyCompetitionResult.EndingRank))
							);
				}
				chartSeriesList.Add(chartSeries);
			}

			LadderCompetitionVM ladderCompetitionVM = new LadderCompetitionVM();
			ladderCompetitionVM.ChartData = JsonConvert.SerializeObject(chartSeriesList);
			return View(ladderCompetitionVM);
		}
	}

	//DataContract for Serializing Data - required to serve in JSON format
	[DataContract]
	public class DataPoint
	{
		public DataPoint(string label, double y)
		{
			this.Label = label;
			this.Y = y;
		}

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "label")]
		public string Label = "";

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "y")]
		public Nullable<double> Y = null;
	}

	[DataContract]
	public class ChartSeries
	{
		public ChartSeries(string name)
		{
			this.Name = name;
		}


		[DataMember(Name = "type")]
		public const string Type = "line";

		[DataMember(Name = "showInLegend")]
		public const bool ShowInLegend = true;

		//Explicitly setting the name to be used while serializing to JSON.
		[DataMember(Name = "name")]
		public string Name = "";

		[DataMember(Name = "dataPoints")]
		public ICollection<DataPoint> DataPoints;
	}
}


