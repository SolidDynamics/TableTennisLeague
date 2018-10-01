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

		public EloMatchResultVM AllTimeMatchResult { get; set; }
		public LadderMatchResultVM MonthlyMatchResult { get; internal set; }
	}
}