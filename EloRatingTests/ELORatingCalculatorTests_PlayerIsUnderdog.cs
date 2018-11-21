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
	public class ELORatingCalculatorTests_PlayerIsUnderdog
	{
		#region PlayerWins
		[TestMethod]
		public void ReturnsCorrectOpeningRatingForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(1000, ((EloResultCompetitor<string>)results.Player1).StartRating);
		}
		[TestMethod]
		public void ReturnsCorrectTransformedRatingForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(316.22776601683793319988935444327, ((EloResultCompetitor<string>)results.Player1).TransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentTransformedRatingForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(1000, ((EloResultCompetitor<string>)results.Player1).OpponentTransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentRatingForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(1200, ((EloResultCompetitor<string>)results.Player1).OpponentRating);
		}
		[TestMethod]
		public void ReturnsCorrectExpectedScoreForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(0.24, Math.Round(((EloResultCompetitor<string>)results.Player1).ExpectedScore, 2));
		}
		[TestMethod]
		public void ReturnsCorrectKFactorForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(30, ((EloResultCompetitor<string>)results.Player1).KFactor);
		}
		[TestMethod]
		public void ReturnsCorrectActualScoreForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(1, ((EloResultCompetitor<string>)results.Player1).ActualScore);
		}
		[TestMethod]
		public void ReturnsCorrectClosingRatingForWinner()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player1Won);

			Assert.AreEqual(1023, ((EloResultCompetitor<string>)results.Player1).EndRating);
		}
		#endregion


		#region PlayerLoses
		[TestMethod]
		public void ReturnsCorrectOpeningRatingForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(1000, ((EloResultCompetitor<string>)results.Player1).StartRating);
		}

		[TestMethod]
		public void ReturnsCorrectTransformedRatingForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(316.22776601683793319988935444327, ((EloResultCompetitor<string>)results.Player1).TransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentTransformedRatingForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(1000, ((EloResultCompetitor<string>)results.Player1).OpponentTransformedRating);
		}
		[TestMethod]
		public void ReturnsCorrectOpponentRatingForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(1200, ((EloResultCompetitor<string>)results.Player1).OpponentRating);
		}
		[TestMethod]
		public void ReturnsCorrectExpectedScoreForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(0.24, Math.Round(((EloResultCompetitor<string>)results.Player1).ExpectedScore, 2));
		}
		[TestMethod]
		public void ReturnsCorrectKFactorForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(30, ((EloResultCompetitor<string>)results.Player1).KFactor);
		}
		[TestMethod]
		public void ReturnsCorrectActualScoreForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(0, ((EloResultCompetitor<string>)results.Player1).ActualScore);
		}
		[TestMethod]
		public void ReturnsCorrectClosingRatingForLoser()
		{
			//   EloRatingCalculator ratingCalc = new EloRatingCalculator(30);
			var results = new EloFixture<string>("Player1", "Player2", 1000, 1200, 30).ToResult(ContestResult.Player2Won);

			Assert.AreEqual(993, ((EloResultCompetitor<string>)results.Player1).EndRating);
		}
		#endregion




	}
}
