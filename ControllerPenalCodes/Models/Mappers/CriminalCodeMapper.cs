using System.Collections.Generic;
using System.Linq;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels;

namespace ControllerPenalCodes.Models.Mappers
{
	public class CriminalCodeMapper
	{
		public static GetCriminalCodeViewModel EntityToViewModel(CriminalCode criminalCode)
		{
			return criminalCode == null ? null : new GetCriminalCodeViewModel
			{
				Id = criminalCode.Id,
				Name = criminalCode.Name,
				Description = criminalCode.Description,
				Penalty = criminalCode.Penalty,
				PrisionTime = criminalCode.PrisionTime,
				Status = criminalCode.Status,
				CreateDate = criminalCode.CreateDate,
				UpdateDate = criminalCode.UpdateDate,
				CreateUser = criminalCode.CreateUser,
				UpdateUser = criminalCode.UpdateUser
			};
		}

		public static IEnumerable<GetCriminalCodeViewModel> EntityListToViewModelList(IEnumerable<CriminalCode> criminalCodeList)
		{
			return criminalCodeList?.Select(criminalCode => EntityToViewModel(criminalCode));
		}
	}
}
