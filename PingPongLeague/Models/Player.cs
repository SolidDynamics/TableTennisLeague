using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PingPongLeague.Models
{
	public class Player
	{
		private const int SEED_RATING = 2000;
		private const int FORM_NO_OF_MATCHES = 5;

		public string FirstName { get; set; }

		public string LastName { get; set; }

		[NotMapped]
		public string FullName => $"{FirstName} {LastName}";

		public virtual ICollection<MatchParticipation> MatchParticipations { get; set; }

		public int PlayerID { get; internal set; }

		public override string ToString() { return FullName; }

		[NotMapped]
		public int AllTimeRating
		{
			get
			{
				var mostRecentRating = MatchParticipations
					.OrderByDescending(mp => mp.Match.DateOfMatch)
					.ThenByDescending(mp => mp.Match.DayMatchOrder)
					.Select(mp => mp.AllTimeCompetitionResult)
					.FirstOrDefault();

				return (mostRecentRating == null) ? 2000 : mostRecentRating.ClosingRating;
			}
		}

		[NotMapped]
		public string Form
		{
			get
			{
				var sb = new StringBuilder();
				var formMatches = MatchParticipations
					.OrderByDescending(x => x.Match.DateOfMatch)
					.ThenByDescending(x => x.Match.MatchID)
					.Take(FORM_NO_OF_MATCHES)
					.ToList();
				for (int i = 0; i < FORM_NO_OF_MATCHES; i++)
				{
					var resultChar = formMatches.Select(m => m.Winner ? "W" : "L").ElementAtOrDefault(i);
					if (resultChar == null) resultChar = "-";
					sb.Append($"{resultChar} ");
				};
				return sb.ToString().Trim();
			}
		}

		internal int GetMonthRating(int year, int month)
		{
			var mostRecentRating = MatchParticipations
					.Where(mp => mp.Match.DateOfMatch.Year == year && mp.Match.DateOfMatch.Month == month)
					.OrderByDescending(mp => mp.Match.DateOfMatch)
					.ThenByDescending(mp => mp.Match.DayMatchOrder)
					.Select(mp => mp.MonthlyCompetitionResult)
					.FirstOrDefault();

			return mostRecentRating.EndingRank;
		}
	}
}