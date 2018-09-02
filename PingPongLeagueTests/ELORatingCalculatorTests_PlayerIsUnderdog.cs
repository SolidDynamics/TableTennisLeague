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
	public class ELORatingCalculatorTests_PlayerIsUnderdog
	{
		#region PlayerWins
		[TestMethod]
		public void ReturnsCorrectOpeningRatingForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200, true, 30);

			Assert.AreEqual(1000, results.OpeningRating);
		}
		[TestMethod]
		public void ReturnsCorrectTransformedRatingForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200, true, 30);

			Assert.AreEqual(316.22776601683793319988935444327, results.TransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentTransformedRatingForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,true, 30);

			Assert.AreEqual(1000, results.OpponentTransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentRatingForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,true, 30);

			Assert.AreEqual(1200, results.OpponentRating);
		}
		[TestMethod]
		public void ReturnsCorrectExpectedScoreForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,true, 30);

			Assert.AreEqual(0.24, Math.Round(results.ExpectedScore, 2));
		}
		[TestMethod]
		public void ReturnsCorrectKFactorForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,true, 30);

			Assert.AreEqual(30, results.KFactor);
		}
		[TestMethod]
		public void ReturnsCorrectActualScoreForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,true, 30);

			Assert.AreEqual(1, results.ActualScore);
		}
		[TestMethod]
		public void ReturnsCorrectClosingRatingForWinner()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,true, 30);

			Assert.AreEqual(1023, results.ClosingRating);
		}
		#endregion


		#region PlayerLoses
		[TestMethod]
		public void ReturnsCorrectOpeningRatingForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,false, 30);

			Assert.AreEqual(1000, results.OpeningRating);
		}
		[TestMethod]
		public void ReturnsCorrectTransformedRatingForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,false, 30);

			Assert.AreEqual(316.22776601683793319988935444327, results.TransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentTransformedRatingForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,false, 30);

			Assert.AreEqual(1000, results.OpponentTransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentRatingForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,false, 30);

			Assert.AreEqual(1200, results.OpponentRating);
		}
		[TestMethod]
		public void ReturnsCorrectExpectedScoreForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,false, 30);

			Assert.AreEqual(0.24, Math.Round(results.ExpectedScore, 2));
		}
		[TestMethod]
		public void ReturnsCorrectKFactorForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,false, 30);

			Assert.AreEqual(30, results.KFactor);
		}
		[TestMethod]
		public void ReturnsCorrectActualScoreForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,false, 30);

			Assert.AreEqual(0, results.ActualScore);
		}
		[TestMethod]
		public void ReturnsCorrectClosingRatingForLoser()
		{
			ELORatingCalculator ratingCalc = new ELORatingCalculator();
			var results = ratingCalc.CalculateRatings(1000, 1200,false, 30);

			Assert.AreEqual(993, results.ClosingRating);
		}
		#endregion




	}
}
