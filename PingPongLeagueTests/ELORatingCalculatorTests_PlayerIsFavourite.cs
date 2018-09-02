using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PingPongLeague.Calculators;

namespace PingPongLeagueTests
{
	/// <summary>
	/// Runs tests for the following values based on a player at 2400 playing a player at 2000 in both winning and losing scenarios
	/// OpeningRating 
	/// TransformedRating 
	/// OpponentTransformedRating
	/// OpponentRating 
	/// ExpectedScore 
	/// KFactor 
	/// ActualScore 
	/// ClosingRating 
	/// See: https://metinmediamath.wordpress.com/2013/11/27/how-to-calculate-the-elo-rating-including-example/
	/// </summary>
	[TestClass]
	public class ELORatingCalculatorTests_PlayerIsFavourite
	{
		#region PlayerWins
		[TestMethod]
		public void ReturnsCorrectOpeningRatingForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, true, 32);

			Assert.AreEqual(2400, results.OpeningRating);
		}
		[TestMethod]
		public void ReturnsCorrectTransformedRatingForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, true, 32);

			Assert.AreEqual(1000000, results.TransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentTransformedRatingForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, true, 32);

			Assert.AreEqual(100000, results.OpponentTransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentRatingForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, true, 32);

			Assert.AreEqual(2000, results.OpponentRating);
		}
		[TestMethod]
		public void ReturnsCorrectExpectedScoreForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, true, 32);

			Assert.AreEqual(0.909, Math.Round(results.ExpectedScore, 3));
		}
		[TestMethod]
		public void ReturnsCorrectKFactorForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, true, 32);

			Assert.AreEqual(32, results.KFactor);
		}
		[TestMethod]
		public void ReturnsCorrectActualScoreForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, true, 32);

			Assert.AreEqual(1, results.ActualScore);
		}
		[TestMethod]
		public void ReturnsCorrectClosingRatingForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, true, 32);

			Assert.AreEqual(2403, results.ClosingRating);
		}
		#endregion


		#region PlayerLoses
		[TestMethod]
		public void ReturnsCorrectOpeningRatingForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, false, 32);

			Assert.AreEqual(2400, results.OpeningRating);
		}
		[TestMethod]
		public void ReturnsCorrectTransformedRatingForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, false, 32);

			Assert.AreEqual(1000000, results.TransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentTransformedRatingForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, false, 32);

			Assert.AreEqual(100000, results.OpponentTransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentRatingForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, false, 32);

			Assert.AreEqual(2000, results.OpponentRating);
		}
		[TestMethod]
		public void ReturnsCorrectExpectedScoreForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, false, 32);

			Assert.AreEqual(0.909, Math.Round(results.ExpectedScore, 3));
		}
		[TestMethod]
		public void ReturnsCorrectKFactorForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, false, 32);

			Assert.AreEqual(32, results.KFactor);
		}
		[TestMethod]
		public void ReturnsCorrectActualScoreForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, false, 32);

			Assert.AreEqual(0, results.ActualScore);
		}
		[TestMethod]
		public void ReturnsCorrectClosingRatingForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(2400, 2000, false, 32);

			Assert.AreEqual(2371, results.ClosingRating);
		}
		#endregion




	}
}
