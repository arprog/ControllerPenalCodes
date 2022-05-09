using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;
using ControllerPenalCodes.Shared;

namespace ControllerPenalCodes.Interfaces.ServiceInterfaces
{
	public interface IUserService
	{
		Task<Response<GetUserViewModel>> Create(CreateUserViewModel userViewModel);

		Task<Response<IEnumerable<GetUserViewModel>>> GetAll();

		Task<Response<GetUserViewModel>> GetById(Guid userId);

		Task<Response<GetUserViewModel>> GetByUsername(string username);

		Task<Response<User>> Update(Guid userId, UpdateUserViewModel newUserViewModel);

		Task<Response<User>> Delete(Guid userId);
	}
}
