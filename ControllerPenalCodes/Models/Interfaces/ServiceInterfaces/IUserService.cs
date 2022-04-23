using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;
using ControllerPenalCodes.Utils;

namespace ControllerPenalCodes.Models.Interfaces.ServiceInterfaces
{
	public interface IUserService
	{
		Task<Response<User>> Create(CreateUserViewModel userViewModel);

		Task<Response<IEnumerable<GetUserViewModel>>> GetAll();

		Task<Response<GetUserViewModel>> GetById(Guid userId);

		Task<Response<GetUserViewModel>> GetByUsername(string username);

		Task<Response<User>> Update(UpdateUserViewModel newUserViewModel);

		Task<Response<User>> Delete(Guid userId);
	}
}
