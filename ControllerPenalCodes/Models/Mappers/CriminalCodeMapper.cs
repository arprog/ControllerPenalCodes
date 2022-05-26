using System.Collections.Generic;
using System.Linq;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels;
using ControllerPenalCodes.Shared;

namespace ControllerPenalCodes.Models.Mappers
{
	public class CriminalCodeMapper
	{
		public static GetUniqueCriminalCodeViewModel EntityToUniqueViewModel(CriminalCode criminalCode)
		{
			return criminalCode == null ? null : new GetUniqueCriminalCodeViewModel
			{
				Id = criminalCode.Id,
				Name = criminalCode.Name,
				Description = criminalCode.Description,
				Penalty = criminalCode.Penalty,
				PrisionTime = criminalCode.PrisionTime,
				Status = StatusMapper.EntityToViewModel(criminalCode.Status),
				CreateDate = criminalCode.CreateDate,
				UpdateDate = criminalCode.UpdateDate,
				CreateUser = UserMapper.EntityToViewModel(criminalCode.CreateUser),
				UpdateUser = UserMapper.EntityToViewModel(criminalCode.UpdateUser)
			};
		}

		public static GetGenericCriminalCodeViewModel EntityToGenericViewModel(CriminalCode criminalCode)
		{
			return criminalCode == null ? null : new GetGenericCriminalCodeViewModel
			{
				Id = criminalCode.Id,
				Name = criminalCode.Name,
				Penalty = criminalCode.Penalty,
				PrisionTime = criminalCode.PrisionTime,
				Status = StatusMapper.EntityToViewModel(criminalCode.Status),
			};
		}

		public static GetCreatedCriminalCodeViewModel EntityToCreatedViewModel(CriminalCode criminalCode)
		{
			return criminalCode == null ? null : new GetCreatedCriminalCodeViewModel
			{
				Id = criminalCode.Id,
				Name = criminalCode.Name,
				Description = criminalCode.Description,
				Penalty = criminalCode.Penalty,
				PrisionTime = criminalCode.PrisionTime,
				StatusId = criminalCode.StatusId,
				CreateDate = criminalCode.CreateDate,
				UpdateDate = criminalCode.UpdateDate,
				CreateUserId = criminalCode.CreateUserId,
				UpdateUserId = criminalCode.UpdateUserId
			};
		}

		public static Pagination<GetGenericCriminalCodeViewModel> EntityListToGenericViewModelListPaginated(
			int totalItems,
			int amountItemsByPage,
			int currentPage,
			IEnumerable<CriminalCode> criminalCodeList)
		{
			if (criminalCodeList == null || criminalCodeList.Count() == 0)
				return null;

			var criminalCodeViewModelList = criminalCodeList.Select(criminalCode => EntityToGenericViewModel(criminalCode));

			return Pagination<GetGenericCriminalCodeViewModel>.GetPagination(totalItems, amountItemsByPage, currentPage, criminalCodeViewModelList);
		}
	}
}
