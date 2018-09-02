namespace PingPongLeague.Models
{
	public class CompetitionResult
	{
		public int CompetitionResultID { get; set; }

		public virtual Competition Competition { get; set; }

		public int CompetitionID { get; set; }

		public virtual MatchParticipation MatchParticipation { get; set; }
		public int MatchParticipationID { get; set; }

		public Ratings Ratings { get; set; }


	}
}