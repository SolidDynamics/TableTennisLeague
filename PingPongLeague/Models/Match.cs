using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PingPongLeague.Models
{
    public class Match
    {
        public int MatchID { get; set; }
    
        public DateTime DateOfMatch { get; set; }

		public int DayMatchOrder { get; set; }
        
        public override string ToString() { return $"[{DateOfMatch:ddd dd MMM}] {Winner} beat {Loser}"; }

		public virtual ICollection<MatchParticipation> MatchParticipations { get; set; }

		[NotMapped]
		public Player Winner
		{
			get
			{
				return MatchParticipations.Where(mp => mp.Winner).Single().Player;
			}
		}

		[NotMapped]
		public Player Loser
		{
			get
			{
				return MatchParticipations.Where(mp => !mp.Winner).Single().Player;
			}
		}
	}
}
