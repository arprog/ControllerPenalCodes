using System;
using System.ComponentModel.DataAnnotations;

namespace ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels
{
	public class CreateCriminalCodeViewModel
	{
		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Name { get; set; }

		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Description { get; set; }

		[Required]
		[Range(double.Epsilon, double.MaxValue, ErrorMessage = "The value of the 'penalty' must be greater than 0.")]
		public decimal Penalty { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "The value of the 'prisionTime' must be greater than 0.")]
		public int PrisionTime { get; set; }

		[Required]
		public Guid StatusId { get; set; }
	}
}
