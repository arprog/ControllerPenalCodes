using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;

namespace ControllerPenalCodes.Interfaces.RepositoryInterfaces
{
	public interface ICriminalCodeRepository
	{
		Task Add(CriminalCode criminalCode);

		Task<IEnumerable<CriminalCode>> GetAll();

		Task<CriminalCode> GetById(Guid criminalCodeId);

		Task<CriminalCode> GetByName(string criminalCodeName);

		Task Update(CriminalCode criminalCode);

		Task Remove(CriminalCode criminalCode);
	}
}
