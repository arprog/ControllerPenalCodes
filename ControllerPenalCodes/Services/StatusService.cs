using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.Mappers;
using ControllerPenalCodes.Models.ViewModels.StatusViewModels;
using ControllerPenalCodes.Shared;

namespace ControllerPenalCodes.Services
{
	public class StatusService : IStatusService
	{
		private readonly IStatusRepository _statusRepository;

		public StatusService(IStatusRepository statusRepository)
		{
			_statusRepository = statusRepository;
		}

		public async Task<Response<Status>> Create(CreateStatusViewModel statusViewModel)
		{
			var status = await _statusRepository.GetByName(statusViewModel.Name);

			if (status != null)
				return Response<Status>.ResponseService(false, "There is already status registered with the 'name' informed.");

			status = new Status
			{
				Id = Guid.NewGuid(),
				Name = statusViewModel.Name
			};

			await _statusRepository.Add(status);

			return Response<Status>.ResponseService(true, $"api/v1/status/{status.Id}", status);
		}

		public async Task<Response<IEnumerable<GetStatusViewModel>>> GetAll()
		{
			var statusList = await _statusRepository.GetAll();

			if (statusList == null || statusList.Count() == 0)
				return Response<IEnumerable<GetStatusViewModel>>.ResponseService(false);

			var statusViewModelList = StatusMapper.EntityListToViewModelList(statusList);

			return Response<IEnumerable<GetStatusViewModel>>.ResponseService(true, statusViewModelList);
		}

		public async Task<Response<GetStatusViewModel>> GetById(Guid statusId)
		{
			if (statusId == Guid.Empty)
				return Response<GetStatusViewModel>.ResponseService(false, "The 'statusId' is zeroed.");

			var status = await _statusRepository.GetById(statusId);

			if (status == null)
				return Response<GetStatusViewModel>.ResponseService(false);

			var statusViewModel = StatusMapper.EntityToViewModel(status);

			return Response<GetStatusViewModel>.ResponseService(true, statusViewModel);
		}

		public async Task<Response<GetStatusViewModel>> GetByName(string statusName)
		{
			var status = await _statusRepository.GetByName(statusName);

			if (status == null)
				return Response<GetStatusViewModel>.ResponseService(false);

			var statusViewModel = StatusMapper.EntityToViewModel(status);

			return Response<GetStatusViewModel>.ResponseService(true, statusViewModel);
		}

		public async Task<Response<Status>> Update(Guid statusId, UpdateStatusViewModel newStatusViewModel)
		{
			if (statusId == Guid.Empty)
				return Response<Status>.ResponseService(false, "The 'id' is zeroed.");

			var statusById = await _statusRepository.GetById(statusId);

			if (statusById == null)
				return Response<Status>.ResponseService(false, "There is no status registered with the 'id' informed.");

			var statusByName = await _statusRepository.GetOtherStatusByName(statusId, newStatusViewModel.Name);

			if (statusByName != null)
				return Response<Status>.ResponseService(false, "There is already other status registered with the 'name' informed.");

			var newStatus = new Status
			{
				Id = statusById.Id,
				Name = newStatusViewModel.Name
			};

			await _statusRepository.Update(newStatus);

			return Response<Status>.ResponseService(true);
		}

		public async Task<Response<Status>> Delete(Guid statusId)
		{
			if (statusId == Guid.Empty)
				return Response<Status>.ResponseService(false, "The 'id' is zeroed.");

			var status = await _statusRepository.GetById(statusId);

			if (status == null)
				return Response<Status>.ResponseService(false, "There is no status registered with the 'id' informed.");

			await _statusRepository.Remove(status);

			return Response<Status>.ResponseService(true);
		}
	}
}
