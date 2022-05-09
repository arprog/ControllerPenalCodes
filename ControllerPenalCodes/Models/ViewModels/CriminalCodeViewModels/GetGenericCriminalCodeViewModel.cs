using System;
using ControllerPenalCodes.Models.ViewModels.StatusViewModels;

namespace ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels
{
	public class GetGenericCriminalCodeViewModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public decimal Penalty { get; set; }

		public int PrisionTime { get; set; }

		public GetStatusViewModel Status { get; set; }
	}
}
