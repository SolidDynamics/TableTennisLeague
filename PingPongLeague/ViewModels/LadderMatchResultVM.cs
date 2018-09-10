namespace PingPongLeague.ViewModels
{
	public class LadderMatchResultVM : MatchResultBaseVM
	{
		public LadderPlayerMatchResultVM WinnerResults { get; set; }
		public LadderPlayerMatchResultVM LoserResults { get; set; }
	}

	public class LadderPlayerMatchResultVM
	{
		public int OpeningRank { get; set; }
		public int ClosingRank { get; set; }
		public bool MatchQualifiedForLadder { get; set; }
	}
}