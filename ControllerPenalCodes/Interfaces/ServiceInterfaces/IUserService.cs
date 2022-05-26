using System;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;
using ControllerPenalCodes.Shared;

namespace ControllerPenalCodes.Interfaces.ServiceInterfaces
{
	public interface IUserService
	{
		Task<Response<GetUserViewModel>> Create(CreateUserViewModel userViewModel);

		Task<Response<Pagination<GetUserViewModel>>> GetAll(int page, int itemsByPage);

		Task<Response<GetUserViewModel>> Get(Guid userId);

		Task<Response<User>> UpdateUsername(Guid userId, UpdateUserViewModel newUserViewModel);

		Task<Response<User>> UpdatePassword(Guid userId, UpdatePasswordUserViewModel userViewModel);

		Task<Response<User>> Delete(Guid userId);
	}
}
