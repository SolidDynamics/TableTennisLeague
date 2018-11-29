using System;

namespace EloRating
{
	public class EloResult<T> : EloFixture<T>
	{
		public ContestResult Result { get; private set; }

		private EloResult(EloFixture<T> eloFixture, ContestResult result) 
			: base(eloFixture.Player1.Player, eloFixture.Player2.Player, eloFixture.Player1.StartRating, eloFixture.Player2.StartRating, eloFixture.KFactor)
		{
			Result = result; 
		}

		public EloResult(EloFixtureCompetitor<T> player1, EloFixtureCompetitor<T> player2, int kFactor, ContestResult contestResult)
			: base(player1.Player, player2.Player, player1.StartRating, player2.StartRating, kFactor)
		{
			Player1 = new EloResultCompetitor<T>(player1, contestResult);
			Player2 = new EloResultCompetitor<T>(player2, contestResult);
			KFactor = kFactor;
			Result = contestResult;
		}
	}

	public class EloResultCompetitor<T> : EloFixtureCompetitor<T>
	{

	internal EloResultCompetitor(EloFixtureCompetitor<T> playerFixtureCompetitor, ContestResult contestResult)
			: base(playerFixtureCompetitor)
	{
		Result = contestResult;
	}

	public ContestResult Result { get; private set; }

		public double ActualScore
		{
			get
			{
				if (Result == ContestResult.Draw) return 0.5;
				if ((Result == ContestResult.Player1Won && PlayerNumber == PlayerIdentifier.Player1) ||
					(Result == ContestResult.Player2Won && PlayerNumber == PlayerIdentifier.Player2))
				{
					return 1;
				}
				return 0;
			}
		}

		public int EndRating => Convert.ToInt32(Math.Round(base.StartRating + base.KFactor * (ActualScore - base.ExpectedScore)));

		public double SquaredError
		{
			get
			{
				return Math.Pow(ExpectedScore - ActualScore, 2)
						+ Math.Pow((1 - ExpectedScore) - (1 - ActualScore), 2);
			}
		}
	}
}