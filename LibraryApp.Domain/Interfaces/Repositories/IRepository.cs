using LibraryApp.Domain.Entities;
using System.Linq.Expressions;

namespace LibraryApp.Domain.Interfaces.Repositories
{
	public interface IRepository<T> where T : BaseEntity
	{
		//Method to query all entities
		Task<T> GetByIdAsync(Guid id);

		Task<IEnumerable<T>> GetAllAsync();

		Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

		//Method to modify the state of the entity
		Task AddAsync(T entity);

		Task UpdateAsync(T entity);

		Task DeleteAsync(T entity);

		//Persist the changes
		Task<int> SaveChangesAsync();
	}
}