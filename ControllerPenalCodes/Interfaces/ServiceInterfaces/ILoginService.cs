using System.Threading.Tasks;
using ControllerPenalCodes.ViewModels.LoginViewModels;
using ControllerPenalCodes.ViewModels.UserViewModels;
using ControllerPenalCodes.Utils;

namespace ControllerPenalCodes.Interfaces.ServiceInterfaces
{
	public interface ILoginService
	{
		Task<Response<TokenViewModel>> LoginAuthentication(Authentication authentication, CreateUserViewModel userViewModel);
	}
}
