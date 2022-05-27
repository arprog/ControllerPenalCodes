using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;

namespace ControllerPenalCodes.Interfaces.RepositoryInterfaces
{
	public interface ICriminalCodeRepository
	{
		Task Add(CriminalCode criminalCode);

		Task<IEnumerable<CriminalCode>> GetAll(
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
			int itemsByPage);

		Task<CriminalCode> GetOtherCriminalCodeByName(Guid criminalCodeId, string criminalCodeName);

		Task<CriminalCode> GetById(Guid criminalCodeId);

		Task<CriminalCode> GetByName(string criminalCodeName);

		Task<int> GetAmountCriminalCodes();

		Task Update(CriminalCode criminalCode);

		Task Remove(CriminalCode criminalCode);
	}
}
