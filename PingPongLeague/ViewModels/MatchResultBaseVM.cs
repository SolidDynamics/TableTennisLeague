using System;

namespace PingPongLeague.ViewModels
{
	public abstract class MatchResultBaseVM
	{

		public string CompetitionName { get; set; }
		public int CompetitionID { get; internal set; }

		public DateTime MatchDate { get; set; }

		public string WinnerName { get; set; }
		public string LoserName { get; set; }

		public override string ToString() => $"[{MatchDate:ddd dd MMM}] {WinnerName} beat {LoserName} ";

	}
}