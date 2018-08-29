using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

		Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

		void Add(TEntity entity);
		Task AddAsync(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);

		void Commit();
		Task<int> CommitAsync();
	}
}
