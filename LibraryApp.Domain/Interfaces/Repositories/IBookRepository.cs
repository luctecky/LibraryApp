using LibraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Interfaces.Repositories
{
	public interface IBookRepository : IRepository<Book>
	{
		Task<Book> GetByISBNAsync(string isbn);

		Task<IEnumerable<Book>> GetByTitleAsync(string title);

		Task<IEnumerable<Book>> GetByAuthorAsync(string author);

		Task<IEnumerable<Book>> GetAvailableBooksAsync();
	}
}