using System.ComponentModel.DataAnnotations;

namespace ControllerPenalCodes.ViewModels.StatusViewModels
{
	public class CreateStatusViewModel
	{
		[Required(AllowEmptyStrings = false)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Name { get; set; }
	}
}
