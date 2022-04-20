using System;

namespace ControllerPenalCodes.Models.Entities
{
	public class CriminalCode
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public decimal Penalty { get; set; }

		public int PrisionTime { get; set; }

		public Guid StatusId { get; set; }

		public Status Status { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime UpdateDate { get; set; }

		public Guid CreateUserId { get; set; }

		public User CreateUser { get; set; }

		public Guid UpdateUserId { get; set; }

		public User UpdateUser { get; set; }
	}
}
