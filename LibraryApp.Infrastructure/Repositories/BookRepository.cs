using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces.Repositories;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories
{
	public class BookRepository : Repository<Book>, IBookRepository
	{
		public BookRepository(ApplicationDbContext context) : base(context)
		{
		}

		public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
		{
			return await _dbSet.Where(book => book.AvaliableCopies > 0).ToListAsync();
		}

		public async Task<IEnumerable<Book>> GetByAuthorAsync(string author)
		{
			return await _dbSet.Where(book => book.Author == author).ToListAsync();
		}

		public async Task<Book> GetByISBNAsync(string isbn)
		{
			return await _dbSet.FirstOrDefaultAsync(book => book.ISBN == isbn);
		}

		public async Task<IEnumerable<Book>> GetByTitleAsync(string title)
		{
			return await _dbSet.Where(book => book.Title == title).ToListAsync();
		}
	}
}