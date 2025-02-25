using LibraryAppBlazor.Domain.Entities;
using LibraryAppBlazor.Domain.Interfaces;

namespace LibraryAppBlazor.Application.Services
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _bookRepository;

		public BookService(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task<Book> CreateAsync(string title, string author, string isbn, int publicationYear)
		{
			var book = new Book(title, author, isbn, publicationYear);
			await _bookRepository.AddAsync(book);
			return book;
		}

		public async Task DeleteAsync(Guid id)
		{
			await _bookRepository.DeleteAsync(id);
		}

		public Task<IEnumerable<Book>> GetAllAsync()
		{
			return _bookRepository.GetAllAsync();
		}

		public async Task<Book> GetByIdAsync(Guid id)
		{
			return await _bookRepository.GetByIdAsync(id);
		}

		public async Task UpdateAsync(Guid id, string title, string author, string isbn, int publicationYear)
		{
			var book = await _bookRepository.GetByIdAsync(id);
			if (book == null)
				throw new Exception($"Livro não encontrado{id}");

			book.SetTitle(title);
			book.SetAuthor(author);
			book.SetISBN(isbn);
			book.SetPublicationYear(publicationYear);

			await _bookRepository.UpdateAsync(book);
		}
	}
}