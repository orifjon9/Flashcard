using Flashcard.Models.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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

		public Task AddAsync(TEntity entity)
		{
			return _dbContext.Set<TEntity>().AddAsync(entity);
		}

		public void Commit()
		{
			_dbContext.SaveChanges();
		}

		public async Task<int> CommitAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}

		public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbContext.Set<TEntity>().Where(predicate);
		}

		public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
		}

		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
		}


		public void Update(TEntity entity)
		{
			_dbContext.Set<TEntity>().Attach(entity);
			_dbContext.Entry(entity).State = EntityState.Modified;
		}

		public void Delete(TEntity entity)
		{
			_dbContext.Set<TEntity>().Attach(entity);
			_dbContext.Entry(entity).State = EntityState.Deleted;
		}
	}
}
