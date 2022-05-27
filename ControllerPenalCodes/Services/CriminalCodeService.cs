using System;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.Mappers;
using ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels;
using ControllerPenalCodes.Shared;

namespace ControllerPenalCodes.Services
{
	public class CriminalCodeService : ICriminalCodeService
	{
		private readonly ICriminalCodeRepository _criminalCodeRepository;

		private readonly IStatusRepository _statusRepository;

		public CriminalCodeService(ICriminalCodeRepository criminalCodeRepository, IStatusRepository statusRepository)
		{
			_criminalCodeRepository = criminalCodeRepository;
			_statusRepository = statusRepository;
		}

		public async Task<Response<GetCreatedCriminalCodeViewModel>> Create(string creatingUserId, CreateCriminalCodeViewModel criminalCodeViewModel)
		{
			if (string.IsNullOrEmpty(creatingUserId))
				return Response<GetCreatedCriminalCodeViewModel>.ResponseService(false, "Failed to identify authenticated user.");

			var criminalCode = await _criminalCodeRepository.GetByName(criminalCodeViewModel.Name);

			if (criminalCode != null)
				return Response<GetCreatedCriminalCodeViewModel>.ResponseService(false, "There is already criminal code registered with the 'name' informed.");

			if (criminalCodeViewModel.StatusId == Guid.Empty)
				return Response<GetCreatedCriminalCodeViewModel>.ResponseService(false, "The 'statusId' informed is zeroed.");

			var status = await _statusRepository.GetById(criminalCodeViewModel.StatusId);

			if (status == null)
				return Response<GetCreatedCriminalCodeViewModel>.ResponseService(false, "There is no status registered with the 'statusId' informed.");

			var currentDate = DateTime.Now;

			criminalCode = new CriminalCode
			{
				Id = Guid.NewGuid(),
				Name = criminalCodeViewModel.Name,
				Description = criminalCodeViewModel.Description,
				Penalty = criminalCodeViewModel.Penalty,
				PrisionTime = criminalCodeViewModel.PrisionTime,
				StatusId = criminalCodeViewModel.StatusId,
				CreateDate = currentDate,
				UpdateDate = currentDate,
				CreateUserId = Guid.Parse(creatingUserId),
				UpdateUserId = Guid.Parse(creatingUserId)
			};

			await _criminalCodeRepository.Add(criminalCode);

			var createdCriminalCodeViewModel = CriminalCodeMapper.EntityToCreatedViewModel(criminalCode);

			return Response<GetCreatedCriminalCodeViewModel>.ResponseService(true, $"api/v1/criminal-codes/{createdCriminalCodeViewModel.Id}", createdCriminalCodeViewModel);
		}

		public async Task<Response<Pagination<GetGenericCriminalCodeViewModel>>> GetAll(FilterCriminalCodeViewModel criminalCodeViewModel, int page, int itemsByPage)
		{
			var criminalCodeList = await _criminalCodeRepository.GetAll(criminalCodeViewModel, page, itemsByPage);
				
			if (criminalCodeList == null)
				return Response<Pagination<GetGenericCriminalCodeViewModel>>.ResponseService(false);

			var criminalCodeViewModelList = CriminalCodeMapper.EntityListToGenericViewModelListPaginated(await _criminalCodeRepository.GetAmountCriminalCodes(), itemsByPage, page, criminalCodeList);

			return Response<Pagination<GetGenericCriminalCodeViewModel>>.ResponseService(true, criminalCodeViewModelList);
		}

		public async Task<Response<GetUniqueCriminalCodeViewModel>> Get(Guid criminalCodeId)
		{
			var criminalCode = await _criminalCodeRepository.GetById(criminalCodeId);

			if (criminalCode == null)
				return Response<GetUniqueCriminalCodeViewModel>.ResponseService(false);

			var criminalCodeViewModel = CriminalCodeMapper.EntityToUniqueViewModel(criminalCode);

			return Response<GetUniqueCriminalCodeViewModel>.ResponseService(true, criminalCodeViewModel);
		}

		public async Task<Response<CriminalCode>> Update(Guid criminalCodeId, string updatingUserId, UpdateCriminalCodeViewModel newCriminalCodeViewModel)
		{
			if (string.IsNullOrEmpty(updatingUserId))
				return Response<CriminalCode>.ResponseService(false, "Failed to identify authenticated user.");

			if (criminalCodeId == Guid.Empty)
				return Response<CriminalCode>.ResponseService(false, "The 'id' informed is zeroed.");

			var criminalCodeById = await _criminalCodeRepository.GetById(criminalCodeId);

			if (criminalCodeById == null)
				return Response<CriminalCode>.ResponseService(false, "There is no criminal code registered with the 'id' informed.");

			var criminalCodeByName = await _criminalCodeRepository.GetOtherCriminalCodeByName(criminalCodeId, newCriminalCodeViewModel.Name);

			if (criminalCodeByName != null)
				return Response<CriminalCode>.ResponseService(false, "There is already other criminal code registered with the 'name' informed.");

			if (newCriminalCodeViewModel.StatusId == Guid.Empty)
				return Response<CriminalCode>.ResponseService(false, "The 'statusId' informed is zeroed.");

			var status = await _statusRepository.GetById(newCriminalCodeViewModel.StatusId);

			if (status == null)
				return Response<CriminalCode>.ResponseService(false, "There is no status registered with the 'statusId' informed.");

			var newCriminalCode = new CriminalCode
			{
				Id = criminalCodeById.Id,
				Name = newCriminalCodeViewModel.Name,
				Description = newCriminalCodeViewModel.Description,
				Penalty = newCriminalCodeViewModel.Penalty,
				PrisionTime = newCriminalCodeViewModel.PrisionTime,
				StatusId = newCriminalCodeViewModel.StatusId,
				CreateDate = criminalCodeById.CreateDate,
				UpdateDate = DateTime.Now,
				CreateUserId = criminalCodeById.CreateUserId,
				UpdateUserId = Guid.Parse(updatingUserId)
			};

			await _criminalCodeRepository.Update(newCriminalCode);

			return Response<CriminalCode>.ResponseService(true);
		}

		public async Task<Response<CriminalCode>> Delete(Guid criminalCodeId)
		{
			if (criminalCodeId == Guid.Empty)
				return Response<CriminalCode>.ResponseService(false, "The 'id' informed is zeroed.");

			var criminalCode = await _criminalCodeRepository.GetById(criminalCodeId);

			if (criminalCode == null)
				return Response<CriminalCode>.ResponseService(false, "There is no criminal code registered with the 'id' informed.");

			await _criminalCodeRepository.Remove(criminalCode);

			return Response<CriminalCode>.ResponseService(true);
		}
	}
}
