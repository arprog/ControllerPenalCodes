using System;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.Mappers;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;
using ControllerPenalCodes.Shared;

namespace ControllerPenalCodes.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<Response<GetUserViewModel>> Create(CreateUserViewModel userViewModel)
		{
			var user = await _userRepository.GetByUsername(userViewModel.Username);

			if (user != null)
				return Response<GetUserViewModel>.ResponseService(false, "There is already user registered with the 'username' informed.");

			user = new User
			{
				Id = Guid.NewGuid(),
				UserName = userViewModel.Username,
				Password = Security.GenerateHashPassword(userViewModel.Password)
			};

			await _userRepository.Add(user);

			var createdUserViewModel = UserMapper.EntityToViewModel(user);

			return Response<GetUserViewModel>.ResponseService(true, $"api/v1/users/{createdUserViewModel.Id}", createdUserViewModel);
		}

		public async Task<Response<Pagination<GetUserViewModel>>> GetAll(string id, string username, int page, int itemsByPage)
		{
			var userList = await _userRepository.GetAll(id, username, page, itemsByPage);

			if (userList == null)
				return Response<Pagination<GetUserViewModel>>.ResponseService(false);

			var userViewModelList = UserMapper.EntityListToViewModelListPaginated(await _userRepository.GetAmountUsers(), itemsByPage, page, userList);

			return Response<Pagination<GetUserViewModel>>.ResponseService(true, userViewModelList);
		}

		public async Task<Response<GetUserViewModel>> Get(Guid userId)
		{
			if (userId == Guid.Empty)
				return Response<GetUserViewModel>.ResponseService(false, "The 'id' informed is zeroed.");

			var user = await _userRepository.GetById(userId);

			if (user == null)
				return Response<GetUserViewModel>.ResponseService(false);

			var userViewModel = UserMapper.EntityToViewModel(user);

			return Response<GetUserViewModel>.ResponseService(true, userViewModel);
		}

		public async Task<Response<User>> UpdateUsername(Guid userId, UpdateUserViewModel userViewModel)
		{
			if (userId == Guid.Empty)
				return Response<User>.ResponseService(false, "The 'id' informed is zeroed.");

			var user = await _userRepository.GetOtherUserByUsername(userId, userViewModel.Username);

			if (user != null)
				return Response<User>.ResponseService(false, "There is already other user registered with the 'username' informed.");

			user = await _userRepository.GetById(userId);

			if (user == null)
				return Response<User>.ResponseService(false, "There is no user registered with the 'id' informed.");

			user = new User
			{
				Id = user.Id,
				UserName = userViewModel.Username,
				Password = user.Password
			};

			await _userRepository.Update(user);

			return Response<User>.ResponseService(true);
		}

		public async Task<Response<User>> UpdatePassword(Guid userId, UpdatePasswordUserViewModel userViewModel)
		{
			if (userId == Guid.Empty)
				return Response<User>.ResponseService(false, "The 'id' informed is zeroed.");

			var user = await _userRepository.GetById(userId);

			if (user == null)
				return Response<User>.ResponseService(false, "There is no user registered with the 'id' informed.");

			user = new User
			{
				Id = user.Id,
				UserName = user.UserName,
				Password = Security.GenerateHashPassword(userViewModel.Password)
			};

			await _userRepository.Update(user);

			return Response<User>.ResponseService(true);
		}

		public async Task<Response<User>> Delete(Guid userId)
		{
			if (userId == Guid.Empty)
				return Response<User>.ResponseService(false, "The 'id' informed is zeroed.");

			var user = await _userRepository.GetById(userId);

			if (user == null)
				return Response<User>.ResponseService(false, "There is no user registered with the 'id' informed.");

			await _userRepository.Remove(user);

			return Response<User>.ResponseService(true);
		}
	}
}
