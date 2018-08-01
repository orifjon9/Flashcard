using Flashcard.Models.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Flashcard.Repositories.Persistence
{
	public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected readonly FlashcardContext _dbContext;

		public BaseRepository(FlashcardContext context)
		{
			_dbContext = context;
		}

		public void Add(TEntity entity)
		{
			_dbContext.Set<TEntity>().Add(entity);
		}

		public async void AddAsync(TEntity entity)
		{
			await _dbContext.Set<TEntity>().AddAsync(entity);
		}

		public void Commit()
		{
			_dbContext.SaveChanges();
		}

		public Task CommitAsync()
		{
			return _dbContext.SaveChangesAsync();
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbContext.Set<TEntity>().Where(predicate);
		}

		public TEntity Get(int id)
		{
			return _dbContext.Set<TEntity>().Find(id);
		}

		public IEnumerable<TEntity> GetAll()
		{
			return _dbContext.Set<TEntity>().ToList();
		}

		public Task<TEntity> GetAsync(int id)
		{
			return _dbContext.Set<TEntity>().FindAsync(id);
		}

		public void Update(TEntity entity)
		{
			_dbContext.Set<TEntity>().Update(entity);
		}
		public void Delete(TEntity entity)
		{
			_dbContext.Set<TEntity>().Remove(entity);
		}
	}
}
