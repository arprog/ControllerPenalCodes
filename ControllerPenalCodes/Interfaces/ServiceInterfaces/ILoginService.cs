using System.Threading.Tasks;
using ControllerPenalCodes.Models.ViewModels.LoginViewModels;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;
using ControllerPenalCodes.Utils;

namespace ControllerPenalCodes.Interfaces.ServiceInterfaces
{
	public interface ILoginService
	{
		Task<Response<TokenViewModel>> LoginAuthentication(Authentication authentication, CreateUserViewModel userViewModel);
	}
}
