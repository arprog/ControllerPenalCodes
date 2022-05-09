using System.ComponentModel.DataAnnotations;

namespace ControllerPenalCodes.Models.ViewModels.UserViewModels
{
	public class UpdateUserViewModel
	{
		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Username { get; set; }

		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Password { get; set; }
	}
}
