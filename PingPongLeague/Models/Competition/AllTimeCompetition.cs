using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PingPongLeague.Models
{
	public class AllTimeCompetition : Competition
	{
		[Required]
		public int KFactor { get; set; }
	}
}