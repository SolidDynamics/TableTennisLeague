using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PingPongLeague.Models
{
	public class Competition
	{
		public int CompetitionID { get; set; }

		[Required]
		public CompetitionType CompetitionType { get; set; }

		public string Name { get; set; }

		[Required]
		public int KFactor { get; set; }

		public virtual ICollection<CompetitionResult> CompetitionResults { get; set; }
	}

	public enum CompetitionType
	{
		Monthly,
		AllTime
	}
}