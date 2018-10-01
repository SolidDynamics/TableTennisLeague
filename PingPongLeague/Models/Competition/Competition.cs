using System.ComponentModel.DataAnnotations;

namespace PingPongLeague.Models
{
	public abstract class Competition
	{
		public int CompetitionID { get; set; }
		
		[Required]
		public string Name { get; set; }
		
		public string Subtitle { get; set; }
	}
}