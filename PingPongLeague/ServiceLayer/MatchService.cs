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
		private EloRatingCalculator _ratingCalc;
		private const int RATING_SEED_VALUE = 2000;
		private const int ORDER_VALUE_SPACING = 10;
		private Dictionary<int, int> _newPlayerRanksAdded = new Dictionary<int, int>();

		public MatchService()
		{
			db = new LeagueContext();
			_ratingCalc = new EloRatingCalculator();
		}

		public MatchService(LeagueContext leagueContext)
		{
			db = leagueContext;
			_ratingCalc = new EloRatingCalculator();
		}

		public Match GetMatch(int id)
		{
			return GetMatches().Single(m => m.MatchID == id);
		}

		public int CreateMatch(DateTime dateOfMatch, string player1FullName, string player2FullName, MatchWinner winner)
		{
			var allPlayers = db.Players.ToList();
			var player1Record = allPlayers.Where(p => p.FullName == player1FullName).SingleOrDefault();
			var player2Record = allPlayers.Where(p => p.FullName == player2FullName).SingleOrDefault();

			if (player1Record == null) throw new IndexOutOfRangeException($"Cannot find player with name {player1FullName}");
			if (player2Record == null) throw new IndexOutOfRangeException($"Cannot find player with name {player2FullName}");

			int player1ID = player1Record.PlayerID;
			int player2ID = player2Record.PlayerID;

			return CreateMatch(dateOfMatch, player1ID, player2ID, winner);
		}

		public int CreateMatch(DateTime dateOfMatch, int player1, int player2, MatchWinner winner)
		{
			ValidateMatchIsNotHistoric(dateOfMatch);

			if (player1 == player2) throw new Exception("Player 1 and Player 2 must be different players");

			int lastMatchDayOrder = db.Matches.Where(m => DbFunctions.TruncateTime(m.DateOfMatch) == dateOfMatch.Date).Max(m => (int?)m.DayMatchOrder) ?? 0;
			int matchDayOrder = lastMatchDayOrder + ORDER_VALUE_SPACING;
			Match match = new Match() { DateOfMatch = dateOfMatch, DayMatchOrder = matchDayOrder, MatchParticipations = new List<MatchParticipation>() };

			var player1LadderRank = GetCurrentLadderRank(player1, dateOfMatch.Year, dateOfMatch.Month);
			var player2LadderRank = GetCurrentLadderRank(player2, dateOfMatch.Year, dateOfMatch.Month);

			if (player1LadderRank == player2LadderRank) player2LadderRank++;

			match.MatchParticipations.Add(CreateMatchParticipation(player1, player2, (winner == MatchWinner.Player1), dateOfMatch, player1LadderRank, player2LadderRank));
			match.MatchParticipations.Add(CreateMatchParticipation(player2, player1, (winner == MatchWinner.Player2), dateOfMatch, player2LadderRank, player1LadderRank));

			db.Matches.Add(match);
			db.SaveChanges();

			return match.MatchID;
		}

		private void ValidateMatchIsNotHistoric(DateTime dateOfMatch)
		{
			var latestMatchDate = db.Matches.Max(m => DbFunctions.TruncateTime(m.DateOfMatch));

			if (latestMatchDate != null && dateOfMatch.Date < latestMatchDate.Value)
			{
				throw new NotSupportedException("Adding matches in the past is not currently supported");
			}

		}

		private MatchParticipation CreateMatchParticipation(int playerId, int opponentId, bool winner, DateTime matchDate, int playerLadderRank, int opponentLadderRank)
		{
			var matchParticipation = new MatchParticipation() { PlayerID = playerId, Winner = winner };

			foreach (var comp in db.Competitions.ToList())
			{
				if (comp.GetType() == typeof(AllTimeCompetition))
				{
					matchParticipation.AllTimeCompetition = comp;
					matchParticipation.AllTimeCompetitionResult = CalculateEloRatings(playerId, opponentId, winner, (AllTimeCompetition) comp);
				}
				else
				{
					matchParticipation.MonthlyCompetition = comp;
					matchParticipation.MonthlyCompetitionResult = CalculateLadderRankings(
						playerId, opponentId, winner, (MonthlyCompetition)comp, matchDate, playerLadderRank, opponentLadderRank);
				}
			}

			return matchParticipation;
		}


		private LadderResult CalculateLadderRankings(int playerId, int opponentId, bool winner, MonthlyCompetition comp, DateTime matchDate, int playerLadderRank, int opponentLadderRank)
		{
			var ladderResults = new LadderResult
			{
				StartingRank = playerLadderRank
			};

			var rankDifference = Math.Abs(ladderResults.StartingRank - opponentLadderRank);
			ladderResults.QualifiesAsLadderChallenge = rankDifference <= comp.MaxChallengePlaces;
			var playerIsHigherRanked = ladderResults.StartingRank < opponentLadderRank;

			if (ladderResults.QualifiesAsLadderChallenge && ((!winner && playerIsHigherRanked) || (winner && !playerIsHigherRanked)))
			{
				ladderResults.EndingRank = opponentLadderRank;
			}
			else
			{
				ladderResults.EndingRank = ladderResults.StartingRank;
			}

			return ladderResults;
		}

		private int GetCurrentLadderRank(int playerId, int year, int month)
		{
			var mostRecentRating = GetMatchParticipations(year, month, true)
				.Where(mp => mp.PlayerID == playerId)
				.FirstOrDefault();

			if (mostRecentRating == null)
			{
				return GetInitialRanking(playerId, year, month);
			}

			return mostRecentRating.MonthlyCompetitionResult.EndingRank;
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
			var existingResults = GetMatchParticipations(year, month);

			var currentLowestRank = existingResults.Max(mp => (int?)mp.MonthlyCompetitionResult.EndingRank);

			return currentLowestRank + 1 ?? 1;
		}
		
		private EloResult CalculateEloRatings(int playerId, int opponentId, bool winner, AllTimeCompetition competition)
		{
			var ratings = new EloResult();
			var mostRecentRating = GetMatchParticipations(true)
				.Where(mp => mp.PlayerID == playerId)
				.FirstOrDefault();

			var opponentRating = GetMatchParticipations(true)
				.Where(mp => mp.PlayerID == opponentId)
				.FirstOrDefault();

			var openingRating = (mostRecentRating == null) ? RATING_SEED_VALUE : mostRecentRating.AllTimeCompetitionResult.ClosingRating;
			var opptRating = (opponentRating == null) ? RATING_SEED_VALUE : opponentRating.AllTimeCompetitionResult.ClosingRating;

			return _ratingCalc.CalculateRatings(openingRating, opptRating, winner, competition.KFactor);
		}

		public IQueryable<MatchParticipation> GetMatchParticipations(int year, int month, bool orderDesc = false)
		{
			return GetMatchParticipations(orderDesc)
				.Where(mp => mp.Match.DateOfMatch.Month == month && mp.Match.DateOfMatch.Year == year);
		}

		public IQueryable<MatchParticipation> GetMatchParticipations(bool orderDesc = false)
		{
			if (orderDesc)
				return db.MatchParticipations
					.OrderByDescending(m => m.Match.DateOfMatch)
					.ThenByDescending(m => m.Match.DayMatchOrder);

			return db.MatchParticipations
				.OrderBy(m => m.Match.DateOfMatch)
				.ThenBy(m => m.Match.DayMatchOrder);
		}

		public IEnumerable<PlayerVM> GetPlayers()
		{
			return db.Players.ToList().Select(p => new PlayerVM() { FullName = p.FullName, PlayerID = p.PlayerID });
		}

		public IQueryable<Match> GetMatches()
		{
			return db.Matches
				.Include("MatchParticipations")
				.Include("MatchParticipations.AllTimeCompetition")
				.Include("MatchParticipations.MonthlyCompetition")
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