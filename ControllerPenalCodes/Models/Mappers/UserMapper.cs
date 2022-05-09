using System.Collections.Generic;
using System.Linq;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;

namespace ControllerPenalCodes.Models.Mappers
{
	public class UserMapper
	{
		public static GetUserViewModel EntityToViewModel(User user)
		{
			return user == null ? null : new GetUserViewModel
			{
				Id = user.Id,
				Username = user.UserName
			};
		}

		public static IEnumerable<GetUserViewModel> EntityListToViewModelList(IEnumerable<User> userList)
		{
			if (userList == null || userList.Count() == 0)
				return null;

			return userList.Select(user => EntityToViewModel(user));
		}
	}
}
