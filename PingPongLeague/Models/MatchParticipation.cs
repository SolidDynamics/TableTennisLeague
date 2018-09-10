using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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

		public virtual ICollection<CompetitionResult> CompetitionResults { get; set; }
	}
}