using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.ViewModels.CriminalCodeViewModels;
using ControllerPenalCodes.Utils;

namespace ControllerPenalCodes.Interfaces.ServiceInterfaces
{
	public interface ICriminalCodeService
	{
		Task<Response<CriminalCode>> Create(CreateCriminalCodeViewModel criminalCodeViewModel, string creatingUserId);

		Task<Response<IEnumerable<GetCriminalCodeViewModel>>> GetAll();

		Task<Response<GetCriminalCodeViewModel>> GetById(Guid criminalCodeId);

		Task<Response<GetCriminalCodeViewModel>> GetByName(string criminalCodeName);

		Task<Response<CriminalCode>> Update(UpdateCriminalCodeViewModel newCriminalCodeViewModel, string updatingUserId);

		Task<Response<CriminalCode>> Delete(Guid criminalCodeId);
	}
}
