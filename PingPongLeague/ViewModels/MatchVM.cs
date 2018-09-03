using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PingPongLeague.ViewModels
{
	public class MatchVM
	{
		public DateTime MatchDate { get; set; }

		public string WinnerName { get; set; }
		public string LoserName { get; set; }

		public override string ToString() => $"[{MatchDate:ddd dd MMM}] {WinnerName} beat {LoserName} ";

		public IList<CompetitionMatchResultVM> CompetitionResults;

	}

	public class CompetitionMatchResultVM
	{
		public string CompetitionName { get; set; }
		public CompetitionResultVM WinnerCompResult { get; set; }
		public CompetitionResultVM LoserCompResult { get; set; }
		public int CompetitionID { get; internal set; }
	}

	public class CompetitionResultVM
	{
		public string CompetitionName { get; set; }

		public int OpeningRating { get; set; }

		public double TransformedRating { get; set; }

		public double ExpectedScore { get; set; }

		public int ActualScore { get; set; }

		public int KFactor { get; set; }

		public int ClosingRating { get; set; }
	}
}