using System;
using ControllerPenalCodes.Models.Entities;

namespace ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels
{
	public class GetCriminalCodeViewModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Penalty { get; set; }

		public int PrisionTime { get; set; }

		public Status Status { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime UpdateDate { get; set; }

		public User CreateUser { get; set; }

		public User UpdateUser { get; set; }
	}
}
