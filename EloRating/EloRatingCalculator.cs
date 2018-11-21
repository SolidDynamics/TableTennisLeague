using System;

namespace EloRating
{
	//public class EloRatingCalculator
	//{
	//	const int ELO_N_VALUE = 400;
	//	public int KFactor { get; }

	//	public EloRatingCalculator(int kFactor = 32)
	//	{
	//		KFactor = kFactor;
	//	}

	//	public (EloResult<T> Player1Rating, EloResult<T> Player2Rating) CalculateElo<T>(T player1, T player2, int player1Rating, int player2Rating, ContestResult contestResult)
	//	{
	//		var player1FullRatings = GetEloResult(player1, player1Rating, player2Rating, GetPlayerVsOpponentResult(contestResult, PlayerIdentifier.Player1));
	//		var player2FullRatings = GetEloResult(player2, player2Rating, player1Rating, GetPlayerVsOpponentResult(contestResult, PlayerIdentifier.Player2));
	//		return (player1FullRatings, player2FullRatings);
	//	}

	//	private PlayerVsOpponentResult GetPlayerVsOpponentResult(ContestResult contestResult, PlayerIdentifier player)
	//	{
	//		if(contestResult.Equals(ContestResult.Draw))
	//			return PlayerVsOpponentResult.Drew;

	//		if ((contestResult.Equals(ContestResult.Player1Won) && player.Equals(PlayerIdentifier.Player1)) 
	//			|| (contestResult.Equals(ContestResult.Player2Won) && player.Equals(PlayerIdentifier.Player2))) 
	//			return PlayerVsOpponentResult.Won;

	//		return PlayerVsOpponentResult.Lost;
	//	}

	//	private EloResult<T> GetEloResult<T>(T player, int playerRating, int opponentRating, PlayerVsOpponentResult result)
	//	{
	//		var ratings = new EloResult<T>()
	//		{
	//			Player = player,
	//			StartRating = playerRating,
	//			TransformedRating = Math.Pow(10, (double)playerRating / ELO_N_VALUE),
	//			OpponentRating = opponentRating,
	//			OpponentTransformedRating = Math.Pow(10, (double)opponentRating / ELO_N_VALUE)
	//		};

	//		ratings.ExpectedScore = ratings.TransformedRating / (ratings.TransformedRating + ratings.OpponentTransformedRating);
	//		ratings.KFactor = KFactor;
	//		ratings.ActualScore = result == PlayerVsOpponentResult.Drew ? 0.5 : result == PlayerVsOpponentResult.Won ? 1 : 0;
	//		ratings.EndRating = Convert.ToInt32(Math.Round(playerRating + ratings.KFactor * (ratings.ActualScore - ratings.ExpectedScore)));

	//		return ratings;
	//	}
	//}

	public enum ContestResult
	{
		Player1Won,
		Player2Won,
		Draw
	}

	public enum PlayerIdentifier
	{
		Player1,
		Player2
	}
}
