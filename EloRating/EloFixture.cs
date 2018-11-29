using System;

namespace EloRating
{
	public class EloFixture<T>
	{
		public EloFixture(T player1, T player2, int player1Rating, int player2Rating, int kFactor = 32)
		{
			Player1 = new EloFixtureCompetitor<T>(player1, player1Rating, player2Rating, PlayerIdentifier.Player1, kFactor);
			Player2 = new EloFixtureCompetitor<T>(player2, player2Rating, player1Rating, PlayerIdentifier.Player2, kFactor);
			KFactor = kFactor;
		}
		
		public int KFactor { get; internal set; }
		public EloFixtureCompetitor<T> Player1 { get; protected set; }
		public EloFixtureCompetitor<T> Player2 { get; protected set; }

		public EloResult<T> ToResult(ContestResult contestResult)
		{
			return new EloResult<T>(Player1, Player2, KFactor, contestResult);
		}
	}

	public class EloFixtureCompetitor<T>
	{
		const int ELO_N_VALUE = 400;

		public EloFixtureCompetitor(EloFixtureCompetitor<T> playerFixtureCompetitor)
		{
			Player = playerFixtureCompetitor.Player;
			StartRating = playerFixtureCompetitor.StartRating;
			OpponentRating = playerFixtureCompetitor.OpponentRating;
			PlayerNumber = playerFixtureCompetitor.PlayerNumber;
			KFactor = playerFixtureCompetitor.KFactor;
		}

		internal EloFixtureCompetitor(T player, int startRating, int opponentRating, PlayerIdentifier playerNumber, int kFactor)
		{
			Player = player;
			StartRating = startRating;
			OpponentRating = opponentRating;
			PlayerNumber = playerNumber;
			KFactor = kFactor;
		}

		public T Player { get; internal set; }

		public int StartRating { get; internal set; }

		public double TransformedRating => Math.Pow(10, (double)StartRating / ELO_N_VALUE);

		public double ExpectedScore => TransformedRating / (TransformedRating + OpponentTransformedRating);

		public int OpponentRating { get; internal set; }

		public PlayerIdentifier PlayerNumber { get;  private set; }

		public double OpponentTransformedRating => Math.Pow(10, (double)OpponentRating / ELO_N_VALUE);

		public int KFactor { get; internal set; }
	}
}