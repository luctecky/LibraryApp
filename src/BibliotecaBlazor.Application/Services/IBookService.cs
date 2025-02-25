using LibraryAppBlazor.Domain.Entities;

namespace LibraryAppBlazor.Application.Services
{
	public interface IBookService
	{
		Task<Book> GetByIdAsync(Guid id);

		Task<IEnumerable<Book>> GetAllAsync();

		Task<Book> CreateAsync(string title, string author, string isbn, int publicationYear);

		Task UpdateAsync(Guid id, string title, string author, string isbn, int publicationYear);

		Task DeleteAsync(Guid id);
	}
}