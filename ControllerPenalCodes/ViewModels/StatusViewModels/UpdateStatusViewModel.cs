using System;
using System.ComponentModel.DataAnnotations;

namespace ControllerPenalCodes.ViewModels.StatusViewModels
{
	public class UpdateStatusViewModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Name { get; set; }
	}
}
