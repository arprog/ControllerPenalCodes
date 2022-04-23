using System.ComponentModel.DataAnnotations;

namespace ControllerPenalCodes.ViewModels.UserViewModels
{
	public class CreateUserViewModel
	{
		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Username { get; set; }

		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Password { get; set; }
	}
}
