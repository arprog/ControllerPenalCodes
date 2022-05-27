using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels;

namespace ControllerPenalCodes.Interfaces.RepositoryInterfaces
{
	public interface ICriminalCodeRepository
	{
		Task Add(CriminalCode criminalCode);

		Task<IEnumerable<CriminalCode>> GetAll(FilterCriminalCodeViewModel criminalCodeViewModel, int page, int itemsByPage);

		Task<CriminalCode> GetOtherCriminalCodeByName(Guid criminalCodeId, string criminalCodeName);

		Task<CriminalCode> GetById(Guid criminalCodeId);

		Task<CriminalCode> GetByName(string criminalCodeName);

		Task<int> GetAmountCriminalCodes();

		Task Update(CriminalCode criminalCode);

		Task Remove(CriminalCode criminalCode);
	}
}
