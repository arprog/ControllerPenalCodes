using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;

namespace ControllerPenalCodes.Interfaces.RepositoryInterfaces
{
	public interface IUserRepository
	{
		Task Add(User user);

		Task<IEnumerable<User>> GetAll();

		Task<User> GetByLogin(string username, string userPassword);

		Task<User> GetById(Guid userId);

		Task<User> GetByUsername(string username);

		Task Update(User user);

		Task Remove(User user);
	}
}
