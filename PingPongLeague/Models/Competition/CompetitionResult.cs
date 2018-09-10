using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PingPongLeague.Models
{
	public abstract class CompetitionResult
	{
		public virtual Competition Competition { get; set; }

		[Key, Column(Order = 0)]
		public int CompetitionID { get; set; }

		[Key, Column(Order = 1)]
		public int MatchID { get; set; }

		[Key, Column(Order = 2)]
		public bool Winner { get; set; }

		[ForeignKey("MatchID, Winner")]
		public virtual MatchParticipation MatchParticipation { get; set; }

	}
}