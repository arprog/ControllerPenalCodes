using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces;
using ControllerPenalCodes.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControllerPenalCodes.Repositories
{
	public class StatusRepository : IRepository<Status>
	{
		private readonly DBContext _dbContext;

		public StatusRepository(DBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async void Add(Status status)
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

		public async void Update(Status status)
		{
			_dbContext.Status
				.Update(status);

			await _dbContext
				.SaveChangesAsync();
		}

		public async void Remove(Status status)
		{
			_dbContext.Status
				.Remove(status);

			await _dbContext
				.SaveChangesAsync();
		}
	}
}
