using LibraryAppBlazor.Domain.Entities;

namespace LibraryAppBlazor.Domain.Interfaces
{
	public interface IBookRepository
	{
		//This interface defines the contract for the BookRepository class
		Task<Book> GetByIdAsync(Guid id);

		Task<IEnumerable<Book>> GetAllAsync();

		Task AddAsync(Book book);

		Task UpdateAsync(Book book);

		Task DeleteAsync(Guid id);
	}
}