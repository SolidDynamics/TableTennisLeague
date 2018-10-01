using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PingPongLeague.Models
{
	public class MatchParticipation
	{
		public virtual Player Player { get; set; }
		public int PlayerID { get; set; }

		public virtual Match Match { get; set; }

		[Key, Column(Order = 0)]
		public int MatchID { get; set; }
		[Key, Column(Order = 1)]
		public bool Winner { get; set; }
		
		public EloResult AllTimeCompetitionResult { get; set; }

		public virtual Competition AllTimeCompetition { get; set; }

		public LadderResult MonthlyCompetitionResult { get; set; }

		public virtual Competition MonthlyCompetition { get; set; }
	}



	public class LadderResult
	{
		public int StartingRank { get; set; }

		public bool QualifiesAsLadderChallenge { get; set; }

		public int EndingRank { get; set; }
	}
}