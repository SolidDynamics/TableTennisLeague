using System;
using System.Collections.Generic;
using System.Linq;

namespace EloRating
{
	public class KFactorCalculator<T>
	{
		private readonly List<Contest<T>> _fixtures;
		private readonly int _ignoreFirstXContests;
		private Dictionary<T, PlayerResult> _playerCounter = new Dictionary<T, PlayerResult>();

		public KFactorCalculator(List<Contest<T>> fixtures, int ignoreFirstXContests = 10)
		{
			_fixtures = fixtures;
			_ignoreFirstXContests = ignoreFirstXContests;
		}

		public List<KFactorResult> GetResults(int startValue = 1, int endValue = 100)
		{
			var accuracyResults = new List<KFactorResult>();
			for (int i = startValue; i <= endValue; i++)
			{
				KFactorResult kFactorResult = new KFactorResult()
				{
					KFactor = i,
					Results = new List<double>()
				};
				foreach (var fixture in _fixtures)
				{
					T player1 = fixture.Player1;
					InitializePlayerCounter(player1);
					T player2 = fixture.Player2;
					InitializePlayerCounter(player2);

					var player1NoOfGames = _playerCounter[player1].NumberOfContests;
					var player2NoOfGames = _playerCounter[player2].NumberOfContests;

					_playerCounter[player1].NumberOfContests++;
					_playerCounter[player2].NumberOfContests++;

					var setup = new EloFixture<T>(player1, player2, _playerCounter[player1].EloScore, _playerCounter[player2].EloScore, i);
					var result = setup.ToResult(fixture.Result);
					EloResultCompetitor<T> player1result = ((EloResultCompetitor<T>)result.Player1);
					_playerCounter[player1].EloScore = player1result.EndRating;
					EloResultCompetitor<T> player2result = ((EloResultCompetitor<T>)result.Player2);
					_playerCounter[player2].EloScore = player2result.EndRating;

					if (player1NoOfGames < _ignoreFirstXContests || player2NoOfGames < _ignoreFirstXContests)
						continue;

					var squaredError = Math.Pow((player1result.ExpectedScore - player1result.ActualScore),2)
						+ Math.Pow((player2result.ExpectedScore - player2result.ActualScore), 2);
					kFactorResult.Results.Add(squaredError);
				}
				accuracyResults.Add(kFactorResult);
			}
			return accuracyResults;
		}

		private void InitializePlayerCounter(T player)
		{
			if (!_playerCounter.ContainsKey(player))
			{
				_playerCounter.Add(player, new PlayerResult());
			}
		}

	
	}

	public class KFactorResult
	{
		public int KFactor { get; set; }

		public double AverageError => Results.Average();

		public List<double> Results { get; set; }
	}

	public class PlayerResult
	{
		public int NumberOfContests { get; set; } = 0;

		public int EloScore { get; set; } = 2000;
	}

	public class Contest<T>
	{
		public T Player1 { get; set; }

		public T Player2 { get; set; }

		public ContestResult Result { get; set; }
	}
}
