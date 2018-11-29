using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PingPongLeague.ViewModels
{
	public class MatchCreateModel
	{
		[Required(ErrorMessage = "Date must be entered")]
		[DataType(DataType.Date)]
		public DateTime MatchDate { get; set; }

		[Required(ErrorMessage = "Please select a winner")]
		public int Winner { get; set; }

		[Required(ErrorMessage = "Pleaes select a loser")]
		[PlayersAreDifferent(ErrorMessage = "Loser cannot be the same player as winner" )]
		public int Loser { get; set; }

		public IEnumerable<PlayerVM> Players { get; set; }
	}

	public class PlayersAreDifferent : ValidationAttribute
	{
		public PlayersAreDifferent()
		{
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			MatchCreateModel matchCreateModel = (MatchCreateModel)validationContext.ObjectInstance;

			if(matchCreateModel.Winner == matchCreateModel.Loser)
			{
				return new ValidationResult(ErrorMessage);
			}

			return ValidationResult.Success;
		}
	}
}