using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;

namespace ControllerPenalCodes.Interfaces.RepositoryInterfaces
{
	public interface IStatusRepository
	{
		Task Add(Status status);

		Task<IEnumerable<Status>> GetAll(int page, int itemsByPage);

		Task<Status> GetOtherStatusByName(Guid statusId, string statusName);

		Task<Status> GetById(Guid statusId);

		Task<Status> GetByName(string statusName);

		Task<int> GetAmountStatus();

		Task Update(Status status);

		Task Remove(Status status);
	}
}
