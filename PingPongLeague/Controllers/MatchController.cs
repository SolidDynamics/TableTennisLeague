using PingPongLeague.Models;
using PingPongLeague.ServiceLayer;
using PingPongLeague.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Runtime.Serialization;

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

			var winnerCompResult = GetPlayerCompResult<AllTimeCompetitionResult>(winner);
			var loserCompResults = GetPlayerCompResult<AllTimeCompetitionResult>(loser);

			matchVM.AllTimeMatchResult = new EloMatchResultVM()
			{
				CompetitionID = winnerCompResult.Competition.CompetitionID,
				CompetitionName = winnerCompResult.Competition.Name,
				WinnerResults = GetEloResultsFromCompResults(winnerCompResult),
				LoserResults = GetEloResultsFromCompResults(loserCompResults),
				WinnerName = matchVM.WinnerName,
				LoserName = matchVM.LoserName
			};

			var winnerLadderCompResult = GetPlayerCompResult<MonthlyCompetitionResult>(winner);
			var loserLadderCompResults = GetPlayerCompResult<MonthlyCompetitionResult>(loser);

			matchVM.MonthlyMatchResult = new LadderMatchResultVM()
			{
				CompetitionID = winnerLadderCompResult.Competition.CompetitionID,
				CompetitionName = winnerLadderCompResult.Competition.Name,
				WinnerResults = GetLadderResultsFromCompResults(winnerLadderCompResult),
				LoserResults = GetLadderResultsFromCompResults(loserLadderCompResults),
				WinnerName = matchVM.WinnerName,
				LoserName = matchVM.LoserName
			};

			return View(matchVM);
		}

		private LadderPlayerMatchResultVM GetLadderResultsFromCompResults(MonthlyCompetitionResult compResult)
		{
			return new LadderPlayerMatchResultVM()
			{
				OpeningRank = compResult.Results.StartingRank,
				ClosingRank = compResult.Results.EndingRank,
				MatchQualifiedForLadder = compResult.Results.QualifiesAsLadderChallenge
			};
		}

		private static EloPlayerMatchResultVM GetEloResultsFromCompResults(AllTimeCompetitionResult compResult)
		{
			return new EloPlayerMatchResultVM()
			{
				OpeningRating = compResult.Ratings.OpeningRating,
				TransformedRating = compResult.Ratings.TransformedRating,
				ExpectedScore = compResult.Ratings.ExpectedScore,
				ActualScore = compResult.Ratings.ActualScore,
				KFactor = compResult.Ratings.KFactor,
				ClosingRating = compResult.Ratings.ClosingRating
			};
		}

		private static T GetPlayerCompResult<T>(MatchParticipation participant) where T : CompetitionResult
		{
			var competitionresult = participant.CompetitionResults.OfType<T>().SingleOrDefault();
			if (competitionresult == null) throw new Exception($"Unable to query All Time Competition Result for player {participant.PlayerID} in match {participant.MatchID}");
			return competitionresult;
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

		public ActionResult Ladder(int Year, int Month)
		{
			var chartSeriesList = new List<ChartSeries>();
			
			foreach (var playerWithResults in _matchService.GetMonthlyCompetitionResultsOldestFirst()
				.GroupBy(cr => cr.MatchParticipation.Player, cr => cr, (key, g) => new { Player = key, Results = g }).ToList())
			{
				ChartSeries chartSeries = new ChartSeries(playerWithResults.Player.FullName)
				{
					DataPoints = new List<DataPoint>()
				};
				foreach (var result in playerWithResults.Results)
				{
					chartSeries.DataPoints.Add(
						new DataPoint(
							result.MatchParticipation.Match.DateOfMatch.ToString("dd/MM"),
							Convert.ToDouble(result.Results.EndingRank))
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
