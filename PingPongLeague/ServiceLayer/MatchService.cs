using PingPongLeague.Calculators;
using PingPongLeague.DAL;
using PingPongLeague.Models;
using PingPongLeague.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PingPongLeague.ServiceLayer
{

	public class MatchService
	{

		private LeagueContext db;
		private ELORatingCalculator _ratingCalc;
		private const int RATING_SEED_VALUE = 2000;
		private const int ORDER_VALUE_SPACING = 10;
		private Dictionary<int, int> _newPlayerRanksAdded = new Dictionary<int, int>();

		public MatchService()
		{
			db = new LeagueContext();
			_ratingCalc = new ELORatingCalculator();
		}

		public MatchService(LeagueContext leagueContext)
		{
			db = leagueContext;
			_ratingCalc = new ELORatingCalculator();
		}

		internal Match GetMatch(int id)
		{
			return GetMatches().Single(m => m.MatchID == id);
		}

		public int AddMatch(DateTime dateOfMatch, string player1FullName, string player2FullName, MatchWinner winner)
		{
			int player1ID = db.Players.ToList().Where(p => p.FullName == player1FullName).Single().PlayerID;
			int player2ID = db.Players.ToList().Where(p => p.FullName == player2FullName).Single().PlayerID;

			return AddMatch(dateOfMatch, player1ID, player2ID, winner);
		}

		public int AddMatch(DateTime dateOfMatch, int player1, int player2, MatchWinner winner)
		{
			int lastMatchDayOrder = db.Matches.Where(m => DbFunctions.TruncateTime(m.DateOfMatch) == dateOfMatch.Date).Max(m => (int?)m.DayMatchOrder) ?? 0;
			int matchDayOrder = lastMatchDayOrder + ORDER_VALUE_SPACING;
			Match match = new Match() { DateOfMatch = dateOfMatch, DayMatchOrder = matchDayOrder, MatchParticipations = new List<MatchParticipation>() };

			var player1LadderRank = GetCurrentRanking(player1, dateOfMatch.Year, dateOfMatch.Month);
			var player2LadderRank = GetCurrentRanking(player2, dateOfMatch.Year, dateOfMatch.Month);

			if (player1LadderRank == player2LadderRank) player2LadderRank++;

			match.MatchParticipations.Add(CreateMatchParticipation(player1, player2, (winner == MatchWinner.Player1), dateOfMatch, player1LadderRank, player2LadderRank));
			match.MatchParticipations.Add(CreateMatchParticipation(player2, player1, (winner == MatchWinner.Player2), dateOfMatch, player2LadderRank, player1LadderRank));

			db.Matches.Add(match);
			db.SaveChanges();

			return match.MatchID;
		}

		private MatchParticipation CreateMatchParticipation(int playerId, int opponentId, bool winner, DateTime matchDate, int playerLadderRank, int opponentLadderRank)
		{
			var matchParticipation = new MatchParticipation() { PlayerID = playerId, Winner = winner, CompetitionResults = new List<CompetitionResult>() };

			foreach (var comp in db.Competitions.ToList())
			{
				CompetitionResult compResult;

				if (comp.GetType() == typeof(AllTimeCompetition))
				{
					compResult = CreateAllTimeCompetitionResult(comp as AllTimeCompetition, winner, playerId, opponentId);
				}
				else 
				{
					compResult = GetMonthlyCompetitionResult(comp as MonthlyCompetition, winner, playerId, opponentId, matchDate, playerLadderRank, opponentLadderRank);
				}

				matchParticipation.CompetitionResults.Add(compResult);
			}

			return matchParticipation;
		}

		private MonthlyCompetitionResult GetMonthlyCompetitionResult(MonthlyCompetition comp, bool winner, int playerId, int opponentId, DateTime matchDate, int playerLadderRank, int opponentLadderRank)
		{
			return new MonthlyCompetitionResult()
			{
				Competition = comp,
				Results = CalculateLadderRankings(playerId, opponentId, winner, comp, matchDate, playerLadderRank, opponentLadderRank),
				Winner = winner
			};
		}

		private LadderResults CalculateLadderRankings(int playerId, int opponentId, bool winner, MonthlyCompetition comp, DateTime matchDate, int playerLadderRank, int opponentLadderRank)
		{
			var ladderResults = new LadderResults
			{
				StartingRank = playerLadderRank
			};

			var rankDifference = Math.Abs(ladderResults.StartingRank - opponentLadderRank);
			ladderResults.QualifiesAsLadderChallenge = rankDifference <= comp.MaxChallengePlaces;
			var playerIsHigherRanked = ladderResults.StartingRank < opponentLadderRank;

			if(ladderResults.QualifiesAsLadderChallenge && ((!winner && playerIsHigherRanked) || (winner && !playerIsHigherRanked)))
			{
				ladderResults.EndingRank = opponentLadderRank;
			}
			else
			{
				ladderResults.EndingRank = ladderResults.StartingRank;
			}
				
			return ladderResults;
		}

		private int GetCurrentRanking(int playerId, int year, int month)
		{
			var mostRecentRating = GetMonthlyCompetitionResultsNewestFirst()
			.Where(cr => cr.MatchParticipation.PlayerID == playerId
			&& cr.MatchParticipation.Match.DateOfMatch.Year == year
			&& cr.MatchParticipation.Match.DateOfMatch.Month == month
			)
			.FirstOrDefault();

			if (mostRecentRating == null)
			{
				return GetInitialRanking(playerId, year, month);
			}

			return mostRecentRating.Results.EndingRank;
		}

		/// <summary>
		/// Gets a player's starting ranking for their first game in the Monthly competition
		/// </summary>
		/// <param name="playerId">ID of the player</param>
		/// <param name="year">Year of the competition</param>
		/// <param name="month">Month of the competition</param>
		/// <returns></returns>
		private int GetInitialRanking(int playerId, int year, int month)
		{
			var existingResults = GetMonthlyCompetitionResultsNewestFirst()
			.Where(cr => cr.MatchParticipation.Match.DateOfMatch.Year == year
			&& cr.MatchParticipation.Match.DateOfMatch.Month == month);

			var currentLowestRank = existingResults.Max(cr => (int?)cr.Results.EndingRank);

			return currentLowestRank + 1 ?? 1;
		}

		private AllTimeCompetitionResult CreateAllTimeCompetitionResult(AllTimeCompetition comp, bool winner, int playerId, int opponentId)
		{
			return new AllTimeCompetitionResult()
			{
				Competition = comp,
				Ratings = CalculateEloRatings(playerId, opponentId, winner, comp),
				Winner = winner
			};
		}

		private Ratings CalculateEloRatings(int playerId, int opponentId, bool winner, AllTimeCompetition competition)
		{
			var ratings = new Ratings();
			var mostRecentRating = GetAllTimeCompetitionResultsNewestFirst()
				.Where(cr => cr.MatchParticipation.PlayerID == playerId
				&& cr.Competition.CompetitionID == competition.CompetitionID)
				.FirstOrDefault();

			var opponentRating = GetAllTimeCompetitionResultsNewestFirst()
				.Where(cr => cr.MatchParticipation.PlayerID == opponentId
				&& cr.Competition.CompetitionID == competition.CompetitionID)
				.FirstOrDefault();

			var openingRating = (mostRecentRating == null) ? RATING_SEED_VALUE : mostRecentRating.Ratings.ClosingRating;
			var opptRating = (opponentRating == null) ? RATING_SEED_VALUE : opponentRating.Ratings.ClosingRating;

			return _ratingCalc.CalculateRatings(openingRating, opptRating, winner, competition.KFactor);
		}

		public IQueryable<AllTimeCompetitionResult> GetAllTimeCompetitionResultsOldestFirst()
		{ 
			return db.CompetitionResults.OfType<AllTimeCompetitionResult>()
				.OrderBy(m => m.MatchParticipation.Match.DateOfMatch)
				.ThenBy(m => m.MatchParticipation.Match.DayMatchOrder);
		}

		public IQueryable<AllTimeCompetitionResult> GetAllTimeCompetitionResultsNewestFirst()
		{
			return db.CompetitionResults.OfType<AllTimeCompetitionResult>()
				.OrderByDescending(m => m.MatchParticipation.Match.DateOfMatch)
				.ThenByDescending(m => m.MatchParticipation.Match.DayMatchOrder);
		}

		public IQueryable<MonthlyCompetitionResult> GetMonthlyCompetitionResultsOldestFirst()
		{
			return db.CompetitionResults.OfType<MonthlyCompetitionResult>()
				.OrderBy(m => m.MatchParticipation.Match.DateOfMatch)
				.ThenBy(m => m.MatchParticipation.Match.DayMatchOrder);
		}

		public IQueryable<MonthlyCompetitionResult> GetMonthlyCompetitionResultsNewestFirst()
		{
			return db.CompetitionResults.OfType<MonthlyCompetitionResult>()
				.OrderByDescending(m => m.MatchParticipation.Match.DateOfMatch)
				.ThenByDescending(m => m.MatchParticipation.Match.DayMatchOrder);
		}

		public IEnumerable<PlayerVM> GetPlayers()
		{
			return db.Players.ToList().Select(p => new PlayerVM() { FullName = p.FullName, PlayerID = p.PlayerID });
		}

		public IQueryable<Match> GetMatches()
		{
			return db.Matches
				.Include("MatchParticipations")
				.Include("MatchParticipations.CompetitionResults")
				.Include("MatchParticipations.CompetitionResults.Competition")
				.Include("MatchParticipations.Player")
				.OrderBy(m => m.DateOfMatch)
				.ThenBy(m => m.DayMatchOrder);
		}
	}

	public enum MatchWinner
	{
		Player1,
		Player2
	}
}