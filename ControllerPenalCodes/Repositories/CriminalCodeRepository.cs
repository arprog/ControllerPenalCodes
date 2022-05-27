using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Shared;
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

		public async Task<IEnumerable<CriminalCode>> GetAll(
			string id,
			string name,
			string description,
			decimal? penalty,
			int? prisionTime,
			string statusId,
			DateTime? createDate,
			DateTime? updateDate,
			string createUserId,
			string updateUserId,
			int page,
			int itemsByPage)
		{
			var criminalCodesQuery = _dbContext.CriminalCodes
				.AsNoTracking()
				.Include(criminalCode => criminalCode.Status)
				.Include(criminalCode => criminalCode.CreateUser)
				.Include(criminalCode => criminalCode.UpdateUser)
				.AsQueryable();

			if (!string.IsNullOrEmpty(id))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.Id.ToString().Contains(id.ToLower()));
			}

			if (!string.IsNullOrEmpty(name))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.Name.ToLower().Contains(name.ToLower()));
			}

			if (!string.IsNullOrEmpty(description))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.Description.ToLower().Contains(description.ToLower()));
			}

			if (penalty != null)
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.Penalty == penalty);
			}

			if (prisionTime != null)
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.PrisionTime == prisionTime);
			}

			if (!string.IsNullOrEmpty(statusId))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.StatusId.ToString().Contains(statusId.ToLower()));
			}

			if (createDate != null)
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.CreateDate == createDate);
			}

			if (updateDate != null)
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.UpdateDate == updateDate);
			}

			if (!string.IsNullOrEmpty(createUserId))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.CreateUserId.ToString().Contains(createUserId.ToLower()));
			}

			if (!string.IsNullOrEmpty(updateUserId))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.UpdateUserId.ToString().Contains(updateUserId.ToLower()));
			}

			int totalItems = await GetAmountCriminalCodes();

			var criminalCodes = await Pagination<CriminalCode>
				.PaginateQuery(page, itemsByPage, totalItems, criminalCodesQuery)
				.ToListAsync();

			return criminalCodes;
		}

		public async Task<CriminalCode> GetOtherCriminalCodeByName(Guid criminalCodeId, string criminalCodeName)
		{
			return await _dbContext.CriminalCodes
				.AsNoTracking()
				.Include(criminalCode => criminalCode.Status)
				.Include(criminalCode => criminalCode.CreateUser)
				.Include(criminalCode => criminalCode.UpdateUser)
				.FirstOrDefaultAsync(criminalCode => !(criminalCode.Id == criminalCodeId)
				&& criminalCode.Name.ToLower() == criminalCodeName.ToLower());
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
				.FirstOrDefaultAsync(criminalCode => criminalCode.Name.ToLower() == criminalCodeName.ToLower());
		}

		public async Task<int> GetAmountCriminalCodes()
		{
			return await _dbContext.CriminalCodes.CountAsync();
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
