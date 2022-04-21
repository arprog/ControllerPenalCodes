using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces;
using ControllerPenalCodes.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControllerPenalCodes.Repositories
{
	public class CriminalCodeRepository : IRepository<CriminalCode>
	{
		private readonly DBContext _dbContext;

		public CriminalCodeRepository(DBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async void Add(CriminalCode criminalCode)
		{
			await _dbContext.CriminalCodes
				.AddRangeAsync(criminalCode);

			await _dbContext
				.SaveChangesAsync();
		}

		public async Task<IEnumerable<CriminalCode>> GetAll()
		{
			return await _dbContext.CriminalCodes
				.AsNoTracking()
				.ToListAsync();
		}

		public async Task<CriminalCode> Get(Guid criminalCodeId)
		{
			return await _dbContext.CriminalCodes
				.AsNoTracking()
				.FirstOrDefaultAsync(criminalCode => criminalCode.Id.Equals(criminalCodeId));
		}
		
		public async void Update(CriminalCode criminalCode)
		{
			_dbContext.CriminalCodes
				.Update(criminalCode);

			await _dbContext
				.SaveChangesAsync();
		}

		public async void Remove(CriminalCode criminalCode)
		{
			_dbContext.CriminalCodes
				.Remove(criminalCode);

			await _dbContext
				.SaveChangesAsync();
		}
	}
}
