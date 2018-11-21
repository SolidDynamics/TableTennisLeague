using System;
using EloRating;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
		//	//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1","Player2",2400, 2000).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(2400, results.Player1.StartRating);
		}
		[TestMethod]
		public void ReturnsCorrectTransformedRatingForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(1000000, results.Player1.TransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentTransformedRatingForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(100000, results.Player1.OpponentTransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentRatingForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(2000, results.Player1.OpponentRating);
		}
		[TestMethod]
		public void ReturnsCorrectExpectedScoreForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(0.909, Math.Round(results.Player1.ExpectedScore, 3));
		}
		[TestMethod]
		public void ReturnsCorrectKFactorForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(32, results.Player1.KFactor);
		}
		[TestMethod]
		public void ReturnsCorrectActualScoreForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(1, ((EloResultCompetitor<string>)results.Player1).ActualScore);
		}
		[TestMethod]
		public void ReturnsCorrectClosingRatingForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(2403, ((EloResultCompetitor<string>)results.Player1).EndRating);
		}
		#endregion


		#region PlayerLoses
		[TestMethod]
		public void ReturnsCorrectOpeningRatingForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(2400, results.Player1.StartRating);
		}
		[TestMethod]
		public void ReturnsCorrectTransformedRatingForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(1000000, results.Player1.TransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentTransformedRatingForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(100000, results.Player1.OpponentTransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentRatingForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(2000, results.Player1.OpponentRating);
		}
		[TestMethod]
		public void ReturnsCorrectExpectedScoreForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(0.909, Math.Round(results.Player1.ExpectedScore, 3));
		}
		[TestMethod]
		public void ReturnsCorrectKFactorForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(32, results.Player1.KFactor);
		}
		[TestMethod]
		public void ReturnsCorrectActualScoreForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(0, ((EloResultCompetitor<string>)results.Player1).ActualScore);
		}
		[TestMethod]
		public void ReturnsCorrectClosingRatingForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(32);
			var results = new EloFixture<string>("Player1", "Player2", 2400, 2000).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(2371, ((EloResultCompetitor<string>)results.Player1).EndRating);
		}
		#endregion




	}
}
