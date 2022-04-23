using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.Mappers;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;
using ControllerPenalCodes.Utils;

namespace ControllerPenalCodes.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<Response<User>> Create(CreateUserViewModel userViewModel)
		{
			var user = await _userRepository.Get(userViewModel.Username);

			if (user != null)
				return Response<User>.ResponseService(false, "There is already user registered with the 'username' informed.");

			user = new User
			{
				Id = Guid.NewGuid(),
				UserName = userViewModel.Username,
				Password = Security.GenerateHashPassword(userViewModel.Password)
			};

			await _userRepository.Add(user);

			return Response<User>.ResponseService(true, $"api/v1/users/{user.Id}", user);
		}

		public async Task<Response<IEnumerable<GetUserViewModel>>> GetAll()
		{
			var userList = await _userRepository.GetAll();

			if (userList == null || userList.Count() == 0)
				return Response<IEnumerable<GetUserViewModel>>.ResponseService(false);

			var userViewModelList = UserMapper.EntityListToViewModelList(userList);

			return Response<IEnumerable<GetUserViewModel>>.ResponseService(true, userViewModelList);
		}

		public async Task<Response<GetUserViewModel>> GetById(Guid userId)
		{
			var user = await _userRepository.Get(userId);

			if (user == null)
				return Response<GetUserViewModel>.ResponseService(false);

			var userViewModel = UserMapper.EntityToViewModel(user);

			return Response<GetUserViewModel>.ResponseService(true, userViewModel);
		}

		public async Task<Response<GetUserViewModel>> GetByUsername(string username)
		{
			var user = await _userRepository.Get(username);

			if (user == null)
				return Response<GetUserViewModel>.ResponseService(false);

			var userViewModel = UserMapper.EntityToViewModel(user);

			return Response<GetUserViewModel>.ResponseService(true, userViewModel);
		}

		public async Task<Response<User>> Update(UpdateUserViewModel newUserViewModel)
		{
			var user = await _userRepository.Get(newUserViewModel.Username);

			if (user != null)
				return Response<User>.ResponseService(false, "There is already user registered with the 'username' informed.");

			user = await _userRepository.Get(newUserViewModel.Id);

			if (user == null)
				return Response<User>.ResponseService(false);

			var newUser = new User
			{
				Id = user.Id,
				UserName = newUserViewModel.Username,
				Password = Security.GenerateHashPassword(newUserViewModel.Password)
			};

			await _userRepository.Update(newUser);

			return Response<User>.ResponseService(true);
		}

		public async Task<Response<User>> Delete(Guid userId)
		{
			var user = await _userRepository.Get(userId);

			if (user == null)
				return Response<User>.ResponseService(false);

			await _userRepository.Remove(user);

			return Response<User>.ResponseService(true);
		}
	}
}
