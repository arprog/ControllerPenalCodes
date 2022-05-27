using System;

namespace ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels
{
	public class FilterCriminalCodeViewModel
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal? Penalty { get; set; }

		public int? PrisionTime { get; set; }

		public string StatusId { get; set; }

		public DateTime? CreateDate { get; set; }

		public DateTime? UpdateDate { get; set; }

		public string CreateUserId { get; set; }

		public string UpdateUserId { get; set; }
	}
}
