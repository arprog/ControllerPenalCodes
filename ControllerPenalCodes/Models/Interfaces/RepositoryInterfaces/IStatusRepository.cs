using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;

namespace ControllerPenalCodes.Models.Interfaces.RepositoryInterfaces
{
	public interface IStatusRepository
	{
		Task Add(Status status);

		Task<IEnumerable<Status>> GetAll();

		Task<Status> Get(Guid statusId);

		Task<Status> Get(string statusName);

		Task Update(Status status);

		Task Remove(Status status);
	}
}
