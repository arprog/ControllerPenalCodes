using System;
using System.ComponentModel.DataAnnotations;

namespace ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels
{
	public class UpdateCriminalCodeViewModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Name { get; set; }

		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Description { get; set; }

		[Required]
		public decimal Penalty { get; set; }

		[Required]
		public int PrisionTime { get; set; }

		[Required]
		public Guid StatusId { get; set; }
	}
}
