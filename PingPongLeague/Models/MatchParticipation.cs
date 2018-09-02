using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PingPongLeague.Models
{
	public class MatchParticipation
	{
		public int MatchParticipationID { get; set; }
		public virtual Player Player { get; set; }
		public int PlayerID { get; set; }

		public virtual Match Match { get; set; }
		public int MatchID { get; set; }

		public bool Winner { get; set; }

		public virtual ICollection<CompetitionResult> CompetitionResults { get; set; }

	}
}