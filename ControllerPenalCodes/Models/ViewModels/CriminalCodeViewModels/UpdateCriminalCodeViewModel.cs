using System;
using System.ComponentModel.DataAnnotations;

namespace ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels
{
	public class UpdateCriminalCodeViewModel
	{
		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Name { get; set; }

		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Description { get; set; }

		[Required]
		[Range(0, double.MaxValue, ErrorMessage = "The value of the 'penalty' must be greater than 0.")]
		public decimal Penalty { get; set; }

		[Required]
		[Range(0, int.MaxValue, ErrorMessage = "The value of the 'prisionTime' must be greater than 0.")]
		public int PrisionTime { get; set; }

		[Required]
		public Guid StatusId { get; set; }
	}
}
