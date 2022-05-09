﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels;
using ControllerPenalCodes.Shared;

namespace ControllerPenalCodes.Interfaces.ServiceInterfaces
{
	public interface ICriminalCodeService
	{
		Task<Response<GetCreatedCriminalCodeViewModel>> Create(string creatingUserId, CreateCriminalCodeViewModel criminalCodeViewModel);

		Task<Response<IEnumerable<GetGenericCriminalCodeViewModel>>> GetAll();

		Task<Response<GetUniqueCriminalCodeViewModel>> Get(Guid criminalCodeId);

		Task<Response<CriminalCode>> Update(Guid criminalCodeId, string updatingUserId, UpdateCriminalCodeViewModel newCriminalCodeViewModel);

		Task<Response<CriminalCode>> Delete(Guid criminalCodeId);
	}
}
