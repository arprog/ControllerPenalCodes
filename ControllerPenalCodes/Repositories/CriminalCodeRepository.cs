using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels;
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

		public async Task<IEnumerable<CriminalCode>> GetAll(FilterCriminalCodeViewModel criminalCodeViewModel, int page, int itemsByPage)
		{
			var criminalCodesQuery = _dbContext.CriminalCodes
				.AsNoTracking()
				.Include(criminalCode => criminalCode.Status)
				.Include(criminalCode => criminalCode.CreateUser)
				.Include(criminalCode => criminalCode.UpdateUser)
				.AsQueryable();

			if (!string.IsNullOrEmpty(criminalCodeViewModel.Id))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.Id.ToString().Contains(criminalCodeViewModel.Id.ToLower()));
			}

			if (!string.IsNullOrEmpty(criminalCodeViewModel.Name))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.Name.ToLower().Contains(criminalCodeViewModel.Name.ToLower()));
			}

			if (!string.IsNullOrEmpty(criminalCodeViewModel.Description))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.Description.ToLower().Contains(criminalCodeViewModel.Description.ToLower()));
			}

			if (criminalCodeViewModel.Penalty != null)
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.Penalty == criminalCodeViewModel.Penalty);
			}

			if (criminalCodeViewModel.PrisionTime != null)
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.PrisionTime == criminalCodeViewModel.PrisionTime);
			}

			if (!string.IsNullOrEmpty(criminalCodeViewModel.StatusId))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.StatusId.ToString().Contains(criminalCodeViewModel.StatusId.ToLower()));
			}

			if (criminalCodeViewModel.CreateDate != null)
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.CreateDate == criminalCodeViewModel.CreateDate);
			}

			if (criminalCodeViewModel.UpdateDate != null)
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.UpdateDate == criminalCodeViewModel.UpdateDate);
			}

			if (!string.IsNullOrEmpty(criminalCodeViewModel.CreateUserId))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.CreateUserId.ToString().Contains(criminalCodeViewModel.CreateUserId.ToLower()));
			}

			if (!string.IsNullOrEmpty(criminalCodeViewModel.UpdateUserId))
			{
				criminalCodesQuery = criminalCodesQuery
					.Where(criminalCode => criminalCode.UpdateUserId.ToString().Contains(criminalCodeViewModel.UpdateUserId.ToLower()));
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
