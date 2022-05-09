using System;

namespace ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels
{
	public class GetCreatedCriminalCodeViewModel
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Penalty { get; set; }

		public int PrisionTime { get; set; }

		public Guid StatusId { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime UpdateDate { get; set; }

		public Guid CreateUserId { get; set; }

		public Guid UpdateUserId { get; set; }
	}
}
