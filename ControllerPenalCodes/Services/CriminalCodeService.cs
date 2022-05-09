using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<Response<CriminalCode>> Create(string creatingUserId, CreateCriminalCodeViewModel criminalCodeViewModel)
		{
			if (string.IsNullOrEmpty(creatingUserId))
				return Response<CriminalCode>.ResponseService(false, "Failed to identify authenticated user.");

			var criminalCode = await _criminalCodeRepository.GetByName(criminalCodeViewModel.Name);

			if (criminalCode != null)
				return Response<CriminalCode>.ResponseService(false, "There is already criminal code registered with the 'name' informed.");

			if (criminalCodeViewModel.StatusId == Guid.Empty)
				return Response<CriminalCode>.ResponseService(false, "The 'statusId' is zeroed.");

			var status = await _statusRepository.GetById(criminalCodeViewModel.StatusId);

			if (status == null)
				return Response<CriminalCode>.ResponseService(false, "There is no status registered with the 'statusId' informed.");

			criminalCode = new CriminalCode
			{
				Id = Guid.NewGuid(),
				Name = criminalCodeViewModel.Name,
				Description = criminalCodeViewModel.Description,
				Penalty = criminalCodeViewModel.Penalty,
				PrisionTime = criminalCodeViewModel.PrisionTime,
				StatusId = criminalCodeViewModel.StatusId,
				CreateDate = DateTime.Now,
				UpdateDate = DateTime.Now,
				CreateUserId = Guid.Parse(creatingUserId),
				UpdateUserId = Guid.Parse(creatingUserId)
			};

			await _criminalCodeRepository.Add(criminalCode);

			return Response<CriminalCode>.ResponseService(true, $"api/v1/criminal-codes/{criminalCode.Id}", criminalCode);
		}

		public async Task<Response<IEnumerable<GetGenericCriminalCodeViewModel>>> GetAll()
		{
			var criminalCodeList = await _criminalCodeRepository.GetAll();
				
			if (criminalCodeList == null || criminalCodeList.Count() == 0)
				return Response<IEnumerable<GetGenericCriminalCodeViewModel>>.ResponseService(false);

			var criminalCodeViewModelList = CriminalCodeMapper.EntityListToGenericViewModelList(criminalCodeList);

			return Response<IEnumerable<GetGenericCriminalCodeViewModel>>.ResponseService(true, criminalCodeViewModelList);
		}

		public async Task<Response<CriminalCode>> Update(Guid criminalCodeId, string updatingUserId, UpdateCriminalCodeViewModel newCriminalCodeViewModel)
		{
			if (string.IsNullOrEmpty(updatingUserId))
				return Response<CriminalCode>.ResponseService(false, "Failed to identify authenticated user.");

			if (criminalCodeId == Guid.Empty)
				return Response<CriminalCode>.ResponseService(false, "The 'id' is zeroed.");

			var criminalCodeById = await _criminalCodeRepository.GetById(criminalCodeId);

			if (criminalCodeById == null)
				return Response<CriminalCode>.ResponseService(false, "There is no criminal code registered with the 'id' informed.");

			var criminalCodeByName = await _criminalCodeRepository.GetOtherCriminalCodeByName(criminalCodeId, newCriminalCodeViewModel.Name);

			if (criminalCodeByName != null)
				return Response<CriminalCode>.ResponseService(false, "There is already other criminal code registered with the 'name' informed.");

			if (newCriminalCodeViewModel.StatusId == Guid.Empty)
				return Response<CriminalCode>.ResponseService(false, "The 'statusId' is zeroed.");

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
				return Response<CriminalCode>.ResponseService(false, "The 'id' is zeroed.");

			var criminalCode = await _criminalCodeRepository.GetById(criminalCodeId);

			if (criminalCode == null)
				return Response<CriminalCode>.ResponseService(false, "There is no criminal code registered with the 'id' informed.");

			await _criminalCodeRepository.Remove(criminalCode);

			return Response<CriminalCode>.ResponseService(true);
		}
	}
}
