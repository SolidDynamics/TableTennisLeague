using System;
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
					.SelectMany(mp => mp.CompetitionResults)
					.Where(cr => cr.Competition.CompetitionType == CompetitionType.AllTime)
					.OrderByDescending(cr => cr.MatchParticipation.Match.DateOfMatch)
					.ThenByDescending(cr => cr.MatchParticipation.MatchID)
					.FirstOrDefault();

				return (mostRecentRating == null) ? 2000 : mostRecentRating.Ratings.ClosingRating;
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
				.SelectMany(mp => mp.CompetitionResults)
				.Where(cr => cr.Competition.CompetitionType == CompetitionType.Monthly)
				.Where(cr => cr.MatchParticipation.Match.DateOfMatch.Year == year)
				.Where(cr => cr.MatchParticipation.Match.DateOfMatch.Month == month)
				.OrderByDescending(cr => cr.MatchParticipation.Match.DateOfMatch)
				.ThenByDescending(cr => cr.MatchParticipation.MatchID)
				.FirstOrDefault();

			return (mostRecentRating == null) ? 2000 : mostRecentRating.Ratings.ClosingRating;
		}
	}
}