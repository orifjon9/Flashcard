using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flashcard.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		IEnumerable<TEntity> GetAll();
		IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

		TEntity Get(int id);
		Task<TEntity> GetAsync(int id);

		void Add(TEntity entity);
		void AddAsync(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);

		void Commit();
		Task CommitAsync();
	}
}
