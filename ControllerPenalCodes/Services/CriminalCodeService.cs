using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.Mappers;
using ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels;
using ControllerPenalCodes.Utils;

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

		public async Task<Response<CriminalCode>> Create(CreateCriminalCodeViewModel criminalCodeViewModel, string creatingUserId)
		{
			if (String.IsNullOrEmpty(creatingUserId))
				return Response<CriminalCode>.ResponseService(false, "Failed to identify authenticated user.");

			var criminalCode = await _criminalCodeRepository.Get(criminalCodeViewModel.Name);

			if (criminalCode != null)
				return Response<CriminalCode>.ResponseService(false, "There is already criminal code registered with the 'name' informed.");

			if (criminalCode.Status == null || criminalCode.Status.Id == Guid.Empty)
				throw new ArgumentException("Error mapping database data.");

			var status = await _statusRepository.Get(criminalCode.Status.Id);

			if (status == null)
				return Response<CriminalCode>.ResponseService(false, "There is no status registered with the 'statusId' informed.");

			if (criminalCodeViewModel.Penalty <= 0 || criminalCodeViewModel.PrisionTime <= 0)
				return Response<CriminalCode>.ResponseService(false, "The value of the 'penalty' and 'prisonTime' must be greater than 0.");

			criminalCode = new CriminalCode
			{
				Id = Guid.NewGuid(),
				Name = criminalCodeViewModel.Name,
				Description = criminalCodeViewModel.Description,
				Penalty = criminalCodeViewModel.Penalty,
				PrisionTime = criminalCodeViewModel.PrisionTime,
				StatusId = status.Id,
				CreateDate = DateTime.Now,
				UpdateDate = DateTime.Now,
				CreateUserId = Guid.Parse(creatingUserId),
				UpdateUserId = Guid.Parse(creatingUserId)
			};

			await _criminalCodeRepository.Add(criminalCode);

			return Response<CriminalCode>.ResponseService(true, $"api/v1/criminal-codes/{criminalCode.Id}", criminalCode);
		}

		public async Task<Response<IEnumerable<GetCriminalCodeViewModel>>> GetAll()
		{
			var criminalCodeList = await _criminalCodeRepository.GetAll();
				
			if (criminalCodeList == null || criminalCodeList.Count() == 0)
				return Response<IEnumerable<GetCriminalCodeViewModel>>.ResponseService(false);

			var criminalCodeViewModelList = CriminalCodeMapper.EntityListToViewModelList(criminalCodeList);

			return Response<IEnumerable<GetCriminalCodeViewModel>>.ResponseService(true, criminalCodeViewModelList);
		}

		public async Task<Response<GetCriminalCodeViewModel>> GetById(Guid criminalCodeId)
		{
			var criminalCode = await _criminalCodeRepository.Get(criminalCodeId);

			if (criminalCode == null)
				return Response<GetCriminalCodeViewModel>.ResponseService(false);

			var criminalCodeViewModel = CriminalCodeMapper.EntityToViewModel(criminalCode);

			return Response<GetCriminalCodeViewModel>.ResponseService(true, criminalCodeViewModel);
		}

		public async Task<Response<GetCriminalCodeViewModel>> GetByName(string criminalCodeName)
		{
			var criminalCode = await _criminalCodeRepository.Get(criminalCodeName);

			if (criminalCode == null)
				return Response<GetCriminalCodeViewModel>.ResponseService(false);

			var criminalCodeViewModel = CriminalCodeMapper.EntityToViewModel(criminalCode);

			return Response<GetCriminalCodeViewModel>.ResponseService(true, criminalCodeViewModel);
		}

		public async Task<Response<CriminalCode>> Update(UpdateCriminalCodeViewModel newCriminalCodeViewModel, string updatingUserId)
		{
			if (String.IsNullOrEmpty(updatingUserId))
				return Response<CriminalCode>.ResponseService(false, "Failed to identify authenticated user.");

			var criminalCode = await _criminalCodeRepository.Get(newCriminalCodeViewModel.Name);

			if (criminalCode != null)
				return Response<CriminalCode>.ResponseService(false, "There is already criminal code registered with the 'name' informed.");

			criminalCode = await _criminalCodeRepository.Get(newCriminalCodeViewModel.Id);

			if (criminalCode == null)
				return Response<CriminalCode>.ResponseService(false);

			if (criminalCode.Status == null || criminalCode.Status.Id == Guid.Empty)
				throw new ArgumentException("Error mapping database data.");

			var status = await _statusRepository.Get(criminalCode.Status.Id);

			if (status == null)
				return Response<CriminalCode>.ResponseService(false);

			if (newCriminalCodeViewModel.Penalty <= 0 || newCriminalCodeViewModel.PrisionTime <= 0)
				return Response<CriminalCode>.ResponseService(false, "The value of the 'penalty' and 'prisonTime' must be greater than 0.");

			var newCriminalCode = new CriminalCode
			{
				Id = criminalCode.Id,
				Name = criminalCode.Name,
				Description = newCriminalCodeViewModel.Description,
				Penalty = newCriminalCodeViewModel.Penalty,
				PrisionTime = newCriminalCodeViewModel.PrisionTime,
				StatusId = newCriminalCodeViewModel.Id,
				CreateDate = criminalCode.CreateDate,
				UpdateDate = DateTime.Now,
				CreateUserId = criminalCode.CreateUser.Id,
				UpdateUserId = Guid.Parse(updatingUserId)
			};

			await _criminalCodeRepository.Update(newCriminalCode);

			return Response<CriminalCode>.ResponseService(true);
		}

		public async Task<Response<CriminalCode>> Delete(Guid criminalCodeId)
		{
			var criminalCode = await _criminalCodeRepository.Get(criminalCodeId);

			if (criminalCode == null)
				return Response<CriminalCode>.ResponseService(false);

			await _criminalCodeRepository.Remove(criminalCode);

			return Response<CriminalCode>.ResponseService(true);
		}
	}
}
