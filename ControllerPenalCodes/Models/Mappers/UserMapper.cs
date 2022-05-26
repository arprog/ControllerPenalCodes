using System.Collections.Generic;
using System.Linq;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;
using ControllerPenalCodes.Shared;

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

		public static Pagination<GetUserViewModel> EntityListToViewModelListPaginated(
			int totalItems,
			int amountItemsByPage,
			int currentPage,
			IEnumerable<User> userList)
		{
			if (userList == null || userList.Count() == 0)
				return null;

			var userViewModelList = userList.Select(user => EntityToViewModel(user));

			return Pagination<GetUserViewModel>.GetPagination(totalItems, amountItemsByPage, currentPage, userViewModelList);
		}
	}
}
