using System;
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

		public async Task<Response<Pagination<GetStatusViewModel>>> GetAll(FilterStatusViewModel statusViewModel, int page, int itemsByPage)
		{
			var statusList = await _statusRepository.GetAll(statusViewModel, page, itemsByPage);

			if (statusList == null)
				return Response<Pagination<GetStatusViewModel>>.ResponseService(false);

			var statusViewModelList = StatusMapper.EntityListToViewModelListPaginated(await _statusRepository.GetAmountStatus(), itemsByPage, page, statusList);

			return Response<Pagination<GetStatusViewModel>>.ResponseService(true, statusViewModelList);
		}

		public async Task<Response<GetStatusViewModel>> Get(Guid statusId)
		{
			if (statusId == Guid.Empty)
				return Response<GetStatusViewModel>.ResponseService(false, "The 'statusId' informed is zeroed.");

			var status = await _statusRepository.GetById(statusId);

			if (status == null)
				return Response<GetStatusViewModel>.ResponseService(false);

			var statusViewModel = StatusMapper.EntityToViewModel(status);

			return Response<GetStatusViewModel>.ResponseService(true, statusViewModel);
		}

		public async Task<Response<Status>> Update(Guid statusId, UpdateStatusViewModel newStatusViewModel)
		{
			if (statusId == Guid.Empty)
				return Response<Status>.ResponseService(false, "The 'id' informed is zeroed.");

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
				return Response<Status>.ResponseService(false, "The 'id' informed is zeroed.");

			var status = await _statusRepository.GetById(statusId);

			if (status == null)
				return Response<Status>.ResponseService(false, "There is no status registered with the 'id' informed.");

			await _statusRepository.Remove(status);

			return Response<Status>.ResponseService(true);
		}
	}
}
