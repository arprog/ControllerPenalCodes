using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Shared;
using ControllerPenalCodes.Models.ViewModels.StatusViewModels;
using System.Linq;

namespace ControllerPenalCodes.Repositories
{
	public class StatusRepository : IStatusRepository
	{
		private readonly DBContext _dbContext;

		public StatusRepository(DBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Add(Status status)
		{
			await _dbContext.Status
				.AddRangeAsync(status);

			await _dbContext
				.SaveChangesAsync();
		}

		public async Task<IEnumerable<Status>> GetAll(FilterStatusViewModel statusViewModel, int page, int itemsByPage)
		{
			var statusQuery = _dbContext.Status
				.AsNoTracking();

			if (!string.IsNullOrEmpty(statusViewModel.Id))
			{
				statusQuery = statusQuery
					.Where(status => status.Id.ToString().Contains(statusViewModel.Id.ToLower()));
			}

			if (!string.IsNullOrEmpty(statusViewModel.Name))
			{
				statusQuery = statusQuery
					.Where(status => status.Name.ToLower().Contains(statusViewModel.Name.ToLower()));
			}

			int totalItems = await GetAmountStatus();

			var status = await Pagination<Status>
				.PaginateQuery(page, itemsByPage, totalItems, statusQuery)
				.ToListAsync();

			return status;
		}

		public async Task<Status> GetOtherStatusByName(Guid statusId, string statusName)
		{
			return await _dbContext.Status
				.AsNoTracking()
				.FirstOrDefaultAsync(status => !(status.Id == statusId)
				&& status.Name.ToLower() == statusName.ToLower());
		}

		public async Task<Status> GetById(Guid statusId)
		{
			return await _dbContext.Status
				.AsNoTracking()
				.FirstOrDefaultAsync(status => status.Id == statusId);
		}

		public async Task<Status> GetByName(string statusName)
		{
			return await _dbContext.Status
				.AsNoTracking()
				.FirstOrDefaultAsync(status => status.Name.ToLower() == statusName.ToLower());
		}

		public async Task<int> GetAmountStatus()
		{
			return await _dbContext.Status.CountAsync();
		}

		public async Task Update(Status status)
		{
			_dbContext.Status
				.Update(status);

			await _dbContext
				.SaveChangesAsync();
		}

		public async Task Remove(Status status)
		{
			_dbContext.Status
				.Remove(status);

			await _dbContext
				.SaveChangesAsync();
		}
	}
}
