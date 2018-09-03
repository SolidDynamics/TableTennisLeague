using PingPongLeague.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace PingPongLeague.ViewModels
{
	public class MatchCreateModel
	{
		[Key]
		public int MatchID { get; set; }
		public DateTime MatchDate { get; set; }
		public Player Winner { get; set; }
		public Player Loser { get; set; }
	}
}