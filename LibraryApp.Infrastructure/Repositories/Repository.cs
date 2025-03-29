using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces.Repositories;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryApp.Infrastructure.Repositories
{
	public class Repository<T> : IRepository<T> where T : BaseEntity
	{
		protected readonly ApplicationDbContext _context;
		protected readonly DbSet<T> _dbSet;

		public Repository(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public Task DeleteAsync(T entity)
		{
			_dbSet.Remove(entity);
			return Task.CompletedTask;
		}

		public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.Where(predicate).ToListAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T> GetByIdAdync(Guid id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}

		public Task UpdateAsync(T entity)
		{
			_context.Entry(entity).State = EntityState.Modified;
			return Task.CompletedTask;
		}
	}
}