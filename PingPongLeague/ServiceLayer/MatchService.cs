using PingPongLeague.Calculators;
using PingPongLeague.DAL;
using PingPongLeague.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PingPongLeague.ServiceLayer
{

	public class MatchService
	{

		private LeagueContext db;
		private ELORatingCalculator _ratingCalc;
		private const int RATING_SEED_VALUE = 2000;

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

		public void AddMatch(DateTime dateOfMatch, int player1, int player2, MatchWinner winner)
		{
			Match match = new Match() { DateOfMatch = dateOfMatch, MatchParticipations = new List<MatchParticipation>() };

			match.MatchParticipations.Add(CreateMatchParticipation(player1,player2, (winner == MatchWinner.Player1)));
			match.MatchParticipations.Add(CreateMatchParticipation(player2,player1, (winner == MatchWinner.Player2)));

			db.Matches.Add(match);
			db.SaveChanges();
		}

		private MatchParticipation CreateMatchParticipation(int playerId, int opponentId, bool winner)
		{
			var matchParticipation = new MatchParticipation() { PlayerID = playerId, Winner = winner, CompetitionResults = new List<CompetitionResult>() };

			foreach (var comp in db.Competitions.ToList())
			{
				CompetitionResult compResult = new CompetitionResult();
				compResult.Competition = comp;
				compResult.Ratings = CalculateRatings(playerId, opponentId, winner, comp);
				matchParticipation.CompetitionResults.Add(compResult);
			}

			return matchParticipation;
		}

		private Ratings CalculateRatings(int playerId, int opponentId, bool winner, Competition competition)
		{
			var ratings = new Ratings();
			var mostRecentRating = GetCompetitionResults()
				.Where(cr => cr.MatchParticipation.PlayerID == playerId && cr.Competition.CompetitionID == competition.CompetitionID)
				.FirstOrDefault();

			var opponentRating = GetCompetitionResults()
				.Where(cr => cr.MatchParticipation.PlayerID == opponentId && cr.Competition.CompetitionID == competition.CompetitionID)
				.FirstOrDefault();

			var openingRating = (mostRecentRating == null) ? RATING_SEED_VALUE : mostRecentRating.Ratings.ClosingRating;
			var opptRating = (opponentRating == null) ? RATING_SEED_VALUE : opponentRating.Ratings.ClosingRating;

			return _ratingCalc.CalculateRatings(openingRating, opptRating, winner, competition.KFactor);
		}

		public IQueryable<CompetitionResult> GetCompetitionResults()
		{
			return db.CompetitionResults.OrderByDescending(cr => cr.MatchParticipation.Match.DateOfMatch)
				.ThenByDescending(cr => cr.MatchParticipation.MatchID);
		}
	}

	public enum MatchWinner
	{
		Player1,
		Player2
	}
}