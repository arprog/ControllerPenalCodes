using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.StatusViewModels;
using ControllerPenalCodes.Shared;

namespace ControllerPenalCodes.Interfaces.ServiceInterfaces
{
	public interface IStatusService
	{
		Task<Response<Status>> Create(CreateStatusViewModel statusViewModel);

		Task<Response<IEnumerable<GetStatusViewModel>>> GetAll();

		Task<Response<GetStatusViewModel>> GetById(Guid statusId);

		Task<Response<GetStatusViewModel>> GetByName(string statusName);

		Task<Response<Status>> Update(Guid statusId, UpdateStatusViewModel newStatusViewModel);

		Task<Response<Status>> Delete(Guid statusId);
	}
}
