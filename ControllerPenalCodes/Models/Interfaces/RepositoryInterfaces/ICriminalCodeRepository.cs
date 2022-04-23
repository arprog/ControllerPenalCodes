﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;

namespace ControllerPenalCodes.Models.Interfaces.RepositoryInterfaces
{
	public interface ICriminalCodeRepository
	{
		Task Add(CriminalCode criminalCode);

		Task<IEnumerable<CriminalCode>> GetAll();

		Task<CriminalCode> Get(Guid criminalCodeId);

		Task<CriminalCode> Get(string criminalCodeName);

		Task Update(CriminalCode criminalCode);

		Task Remove(CriminalCode criminalCode);
	}
}
