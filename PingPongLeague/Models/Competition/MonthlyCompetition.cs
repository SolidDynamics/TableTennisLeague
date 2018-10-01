using System.ComponentModel.DataAnnotations.Schema;

namespace PingPongLeague.Models
{
	public class MonthlyCompetition : Competition
	{
		public int MaxChallengePlaces { get; set; }
	}
}