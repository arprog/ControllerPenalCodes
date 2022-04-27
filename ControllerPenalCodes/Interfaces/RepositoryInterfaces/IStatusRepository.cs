using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Entities;

namespace ControllerPenalCodes.Interfaces.RepositoryInterfaces
{
	public interface IStatusRepository
	{
		Task Add(Status status);

		Task<IEnumerable<Status>> GetAll();

		Task<Status> GetById(Guid statusId);

		Task<Status> GetByName(string statusName);

		Task Update(Status status);

		Task Remove(Status status);
	}
}
