using System.Collections.Generic;
using System.Linq;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.StatusViewModels;

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

		public static IEnumerable<GetStatusViewModel> EntityListToViewModelList(IEnumerable<Status> statusList)
		{
			if (statusList == null || statusList.Count() == 0)
				return null;

			return statusList.Select(status => EntityToViewModel(status));
		}
	}
}
