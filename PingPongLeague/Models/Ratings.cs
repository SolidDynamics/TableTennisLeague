namespace PingPongLeague.Models
{
	public class Ratings
	{
		public int OpeningRating { get; set; }

		public double TransformedRating { get; set; }

		public double ExpectedScore { get; set; }

		public int KFactor { get; set; }

		public int ClosingRating { get; set; }
		public int ActualScore { get; set; }
		public int OpponentRating { get; set; }
		public double OpponentTransformedRating { get; internal set; }
	}
}