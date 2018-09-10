using System;
using System.Collections.Generic;

namespace PingPongLeague.ViewModels
{
	public class MatchCreateModel
	{
		public DateTime MatchDate { get; set; }
		public PlayerVM Winner { get; set; }
		public PlayerVM Loser { get; set; }
		public IEnumerable<PlayerVM> Players { get; set; }
	}
}