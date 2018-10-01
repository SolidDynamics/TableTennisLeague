using System;
using System.Collections.Generic;

namespace PingPongLeague.ViewModels
{
	public class EloMatchResultVM : MatchResultBaseVM
	{
		public EloPlayerMatchResultVM WinnerResults { get; set; }
		public EloPlayerMatchResultVM LoserResults { get; set; }
	}

	public class EloPlayerMatchResultVM
	{
		public bool Winner { get; set; }

		public int OpeningRating { get; set; }

		public double TransformedRating { get; set; }

		public double ExpectedScore { get; set; }

		public int ActualScore { get; set; }

		public int KFactor { get; set; }

		public int ClosingRating { get; set; }
	}
}