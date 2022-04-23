using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using Microsoft.EntityFrameworkCore;
using ControllerPenalCodes.Models.Interfaces.RepositoryInterfaces;

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

		public async Task<IEnumerable<Status>> GetAll()
		{
			return await _dbContext.Status
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<Status> Get(Guid statusId)
		{
			return await _dbContext.Status
				.AsNoTracking()
				.FirstOrDefaultAsync(status => status.Id.Equals(statusId));
		}

		public async Task<Status> Get(string statusName)
		{
			return await _dbContext.Status
				.AsNoTracking()
				.FirstOrDefaultAsync(status => status.Name.Equals(statusName));
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
