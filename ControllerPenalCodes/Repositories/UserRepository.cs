using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces;
using ControllerPenalCodes.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControllerPenalCodes.Repositories
{
	public class UserRepository : IRepository<User>
	{
		private readonly DBContext _dbContext;

		public UserRepository(DBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async void Add(User user)
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

		public async Task<User> Get(Guid userId)
		{
			return await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(user => user.Id.Equals(userId));
		}

		public async void Update(User user)
		{
			_dbContext.Users
				.Update(user);

			await _dbContext
				.SaveChangesAsync();
		}

		public async void Remove(User user)
		{
			_dbContext.Users
				.Remove(user);

			await _dbContext
				.SaveChangesAsync();
		}
	}
}
