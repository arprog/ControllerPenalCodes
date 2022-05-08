using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControllerPenalCodes.Repositories
{
	public class CriminalCodeRepository : ICriminalCodeRepository
	{
		private readonly DBContext _dbContext;

		public CriminalCodeRepository(DBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task Add(CriminalCode criminalCode)
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
				.Include(criminalCode => criminalCode.Status)
				.Include(criminalCode => criminalCode.CreateUser)
				.Include(criminalCode => criminalCode.UpdateUser)
				.ToListAsync();
		}

		public async Task<CriminalCode> GetOtherCriminalCodeByName(Guid criminalCodeId, string criminalCodeName)
		{
			return await _dbContext.CriminalCodes
				.AsNoTracking()
				.Include(criminalCode => criminalCode.Status)
				.Include(criminalCode => criminalCode.CreateUser)
				.Include(criminalCode => criminalCode.UpdateUser)
				.FirstOrDefaultAsync(criminalCode => !(criminalCode.Id == criminalCodeId)
				&& criminalCode.Name == criminalCodeName);
		}

		public async Task<CriminalCode> GetById(Guid criminalCodeId)
		{
			return await _dbContext.CriminalCodes
				.AsNoTracking()
				.Include(criminalCode => criminalCode.Status)
				.Include(criminalCode => criminalCode.CreateUser)
				.Include(criminalCode => criminalCode.UpdateUser)
				.FirstOrDefaultAsync(criminalCode => criminalCode.Id == criminalCodeId);
		}

		public async Task<CriminalCode> GetByName(string criminalCodeName)
		{
			return await _dbContext.CriminalCodes
				.AsNoTracking()
				.Include(criminalCode => criminalCode.Status)
				.Include(criminalCode => criminalCode.CreateUser)
				.Include(criminalCode => criminalCode.UpdateUser)
				.FirstOrDefaultAsync(criminalCode => criminalCode.Name == criminalCodeName);
		}

		public async Task Update(CriminalCode criminalCode)
		{
			_dbContext.CriminalCodes
				.Update(criminalCode);

			await _dbContext
				.SaveChangesAsync();
		}

		public async Task Remove(CriminalCode criminalCode)
		{
			_dbContext.CriminalCodes
				.Remove(criminalCode);

			await _dbContext
				.SaveChangesAsync();
		}
	}
}
