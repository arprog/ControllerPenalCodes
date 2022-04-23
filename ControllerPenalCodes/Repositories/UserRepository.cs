using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;

namespace ControllerPenalCodes.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DBContext _dbContext;

		public UserRepository(DBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Add(User user)
		{
			await _dbContext.Users
				.AddRangeAsync(user);

			await _dbContext
				.SaveChangesAsync();
		}

		public async Task<IEnumerable<User>> GetAll()
		{
			return await _dbContext.Users
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<User> Get(string username, string userPassword)
		{
			return await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(user => user.UserName.Equals(username) && user.Password.Equals(userPassword));
		}

		public async Task<User> Get(Guid userId)
		{
			return await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(user => user.Id.Equals(userId));
		}

		public async Task<User> Get(string username)
		{
			return await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(user => user.UserName.Equals(username));
		}

		public async Task Update(User user)
		{
			_dbContext.Users
				.Update(user);

			await _dbContext
				.SaveChangesAsync();
		}

		public async Task Remove(User user)
		{
			_dbContext.Users
				.Remove(user);

			await _dbContext
				.SaveChangesAsync();
		}
	}
}
