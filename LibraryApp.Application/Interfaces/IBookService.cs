using LibraryApp.Application.DTOs.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Interfaces
{
	public interface IBookService
	{
		Task<IEnumerable<BookDto>> GetAllBooksAsync();

		Task<BookDto> GetBookByIdAsync(Guid id);

		Task<BookDto> GetBookByISBNAsync(string isbn);

		Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(string author);

		Task<IEnumerable<BookDto>> GetBooksByTitleAsync(string title);

		Task<BookDto> CreateBookAsync(CreateBookDto bookDto);

		Task<BookDto> UpdateBookAsync(UpdateBookDto bookDto);

		Task DeleteBookAsync(Guid id);
	}
}