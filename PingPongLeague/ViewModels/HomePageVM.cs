using System.Collections.Generic;

namespace PingPongLeague.ViewModels
{
	public class HomePageVM
	{
		public IEnumerable<LeaderboardPosition> AllTimeLeaderboard { get; internal set; }
		public IEnumerable<LeaderboardPosition> MonthLeaderboard { get; internal set; }
		public Dictionary<int, string> RecentGames { get; internal set; }
	}
}