using LibraryAppBlazor.Domain.Entities;
using LibraryAppBlazor.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAppBlazor.Infrastructure.Data.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly LibraryAppDbContext _context;

		public BookRepository(LibraryAppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Book book)
		{
			await _context.Books.AddAsync(book);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var book = await GetByIdAsync(id);
			if (book != null)
			{
				_context.Books.Remove(book);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Book>> GetAllAsync()
		{
			return await _context.Books.ToListAsync();
		}

		public async Task<Book> GetByIdAsync(Guid id)
		{
			return await _context.Books.FindAsync(id);
		}

		public async Task UpdateAsync(Book book)
		{
			_context.Entry(book).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}