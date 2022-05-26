using System;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.StatusViewModels;
using ControllerPenalCodes.Shared;

namespace ControllerPenalCodes.Interfaces.ServiceInterfaces
{
	public interface IStatusService
	{
		Task<Response<Status>> Create(CreateStatusViewModel statusViewModel);

		Task<Response<Pagination<GetStatusViewModel>>> GetAll(int page, int itemsByPage);

		Task<Response<GetStatusViewModel>> Get(Guid statusId);

		Task<Response<Status>> Update(Guid statusId, UpdateStatusViewModel newStatusViewModel);

		Task<Response<Status>> Delete(Guid statusId);
	}
}
