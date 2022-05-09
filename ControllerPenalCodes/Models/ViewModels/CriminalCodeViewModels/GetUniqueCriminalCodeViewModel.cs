using System;
using ControllerPenalCodes.Models.ViewModels.StatusViewModels;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;

namespace ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels
{
	public class GetUniqueCriminalCodeViewModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Penalty { get; set; }

		public int PrisionTime { get; set; }

		public GetStatusViewModel Status { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime UpdateDate { get; set; }

		public GetUserViewModel CreateUser { get; set; }

		public GetUserViewModel UpdateUser { get; set; }
	}
}
