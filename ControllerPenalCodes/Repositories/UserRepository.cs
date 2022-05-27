using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Shared;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;
using System.Linq;

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

		public async Task<IEnumerable<User>> GetAll(FilterUserViewModel userViewModel, int page, int itemsByPage)
		{
			var usersQuery = _dbContext.Users
				.AsNoTracking();

			if (!string.IsNullOrEmpty(userViewModel.Id))
			{
				usersQuery = usersQuery
					.Where(user => user.Id.ToString().Contains(userViewModel.Id.ToLower()));
			}

			if (!string.IsNullOrEmpty(userViewModel.Username))
			{
				usersQuery = usersQuery
					.Where(user => user.UserName.ToLower().Contains(userViewModel.Username.ToLower()));
			}

			int totalItems = await GetAmountUsers();

			var users = await Pagination<User>
				.PaginateQuery(page, itemsByPage, totalItems, usersQuery)
				.ToListAsync();

			return users;
		}

		public async Task<User> GetOtherUserByUsername(Guid userId, string username)
		{
			return await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(user => !(user.Id == userId)
				&& user.UserName.ToLower() == username.ToLower());
		}

		public async Task<User> GetByLogin(string username, string userPassword)
		{
			return await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(user => user.UserName.ToLower() == username.ToLower()
				&& user.Password.ToLower() == userPassword.ToLower());
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
				.FirstOrDefaultAsync(user => user.UserName.ToLower() == username.ToLower());
		}

		public async Task<int> GetAmountUsers()
		{
			return await _dbContext.Users.CountAsync();
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
