using System.ComponentModel.DataAnnotations.Schema;

namespace PingPongLeague.Models
{
	[Table("MonthlyCompetitionResult")]
	public class MonthlyCompetitionResult : CompetitionResult
	{
		public LadderResults Results { get; set; }

	//	public int MonthlyCompetitionID { get; set; }

		[ForeignKey("CompetitionID")]
		public virtual MonthlyCompetition MonthlyCompetition { get; set; }
	}

	public class LadderResults
	{
		public int StartingRank { get; set; }

		public bool QualifiesAsLadderChallenge { get; set; }

		public int EndingRank { get; set; }
	}
}