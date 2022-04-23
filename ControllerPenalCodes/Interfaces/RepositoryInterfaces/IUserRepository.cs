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

		Task<User> Get(string username, string userPassword);

		Task<User> Get(Guid userId);

		Task<User> Get(string username);

		Task Update(User user);

		Task Remove(User user);
	}
}
