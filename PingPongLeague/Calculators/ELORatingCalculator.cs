using PingPongLeague.Models;
using System;

namespace PingPongLeague.Calculators
{
	public class ELORatingCalculator
	{
		private const int ELO_N_VALUE = 400;

		public Ratings CalculateRatings(int playerRating, int opponentRating, bool playerWon, int KFactor)
		{
			var ratings = new Ratings();

			ratings.OpeningRating = playerRating;

			ratings.TransformedRating = Math.Pow(10,((double)ratings.OpeningRating / ELO_N_VALUE));

			ratings.OpponentRating = opponentRating;

			ratings.OpponentTransformedRating = Math.Pow(10, ((double)ratings.OpponentRating / ELO_N_VALUE));

			ratings.ExpectedScore = ratings.TransformedRating / (ratings.TransformedRating + ratings.OpponentTransformedRating);

			ratings.KFactor = KFactor;

			ratings.ActualScore = playerWon ? 1 : 0;

			ratings.ClosingRating = Convert.ToInt32(Math.Round(ratings.OpeningRating + ratings.KFactor * (ratings.ActualScore - ratings.ExpectedScore)));

			return ratings;
		}
	}
}