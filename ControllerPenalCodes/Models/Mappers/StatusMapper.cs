using System.Collections.Generic;
using System.Linq;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.StatusViewModels;
using ControllerPenalCodes.Shared;

namespace ControllerPenalCodes.Models.Mappers
{
	public class StatusMapper
	{
		public static GetStatusViewModel EntityToViewModel(Status status)
		{
			return status == null ? null : new GetStatusViewModel
			{
				Id = status.Id,
				Name = status.Name
			};
		}

		public static Pagination<GetStatusViewModel> EntityListToViewModelListPaginated(
			int totalItems,
			int amountItemsByPage,
			int currentPage,
			IEnumerable<Status> statusList)
		{
			if (statusList == null || statusList.Count() == 0)
				return null;

			var statusViewModelList = statusList.Select(status => EntityToViewModel(status));

			return Pagination<GetStatusViewModel>.GetPagination(totalItems, amountItemsByPage, currentPage, statusViewModelList);
		}
	}
}
