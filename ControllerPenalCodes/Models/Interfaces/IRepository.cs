using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControllerPenalCodes.Models.Interfaces
{
	public interface IRepository<TEntity> where TEntity : class
	{
		void Add(TEntity entity);
		
		Task<IEnumerable<TEntity>> GetAll();

		Task<TEntity> Get(Guid id);

		void Update(TEntity entity);

		void Remove(TEntity entity);
	}
}
