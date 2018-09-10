using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PingPongLeague.Models
{
	[Table("AllTimeCompetitionResult")]
	public class AllTimeCompetitionResult : CompetitionResult
	{
		public Ratings Ratings { get; set; }

		[Required]
		public int KFactor { get; set; }

		[ForeignKey("CompetitionID")]
		public virtual AllTimeCompetition AllTimeCompetition { get; set; }
	}
}