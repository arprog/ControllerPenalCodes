using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;

namespace ControllerPenalCodes.Interfaces.RepositoryInterfaces
{
	public interface IUserRepository
	{
		Task Add(User user);

		Task<IEnumerable<User>> GetAll(FilterUserViewModel userViewModel, int page, int itemsByPage);

		Task<User> GetOtherUserByUsername(Guid userId, string username);

		Task<User> GetByLogin(string username, string userPassword);

		Task<User> GetById(Guid userId);

		Task<User> GetByUsername(string username);

		Task<int> GetAmountUsers();

		Task Update(User user);

		Task Remove(User user);
	}
}
