using System.ComponentModel.DataAnnotations;

namespace ControllerPenalCodes.Models.ViewModels.UserViewModels
{
	public class CreateUserViewModel
	{
		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string UserName { get; set; }

		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Password { get; set; }
	}
}
