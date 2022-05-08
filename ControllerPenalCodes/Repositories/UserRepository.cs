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

		public async Task<User> GetOtherUserByUsername(Guid userId, string username)
		{
			return await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(user => !(user.Id == userId)
				&& user.UserName == username);
		}

		public async Task<User> GetByLogin(string username, string userPassword)
		{
			return await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(user => user.UserName == username
				&& user.Password == userPassword);
		}

		public async Task<User> GetById(Guid userId)
		{
			return await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(user => user.Id == userId);
		}

		public async Task<User> GetByUsername(string username)
		{
			return await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(user => user.UserName == username);
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
