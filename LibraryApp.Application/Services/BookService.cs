using AutoMapper;
using LibraryApp.Application.DTOs.Book;
using LibraryApp.Application.Interfaces;
using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Exceptions;
using LibraryApp.Domain.Interfaces.Repositories;

namespace LibraryApp.Application.Services
{
	public class BookService : IBookService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public BookService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<BookDto> CreateBookAsync(CreateBookDto bookDto)
		{
			//verify if book already exists
			var existingbook = _unitOfWork.Books.GetByISBNAsync(bookDto.ISBN);
			if (existingbook != null)
				throw new DomainExceptions($"Book ISBN {bookDto.ISBN} already exists.");

			//Create a instance of book
			var book = _mapper.Map<Book>(bookDto);

			//Add book to the repository
			await _unitOfWork.Books.AddAsync(book);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<BookDto>(book);
		}

		public async Task DeleteBookAsync(Guid id)
		{
			var book = await _unitOfWork.Books.GetByIdAsync(id);
			if (book == null)
				throw new DomainExceptions($"Book Id {id} not found.");

			//Verify if book has Active Loans
			var activeLoans = await _unitOfWork.Loans.GetLoansByBookAsync(id);
			if (activeLoans.Any(loans => !loans.IsReturned))
				throw new DomainExceptions($"Book Id {id} has active loans.");

			await _unitOfWork.Books.DeleteAsync(book);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
		{
			var books = await _unitOfWork.Books.GetAllAsync();
			return _mapper.Map<IEnumerable<BookDto>>(books);
		}

		public async Task<BookDto> GetBookByIdAsync(Guid id)
		{
			var book = await _unitOfWork.Books.GetByIdAsync(id);
			if (book == null)
				throw new DomainExceptions($"Book Id {id} not found.");

			return _mapper.Map<BookDto>(book);
		}

		public async Task<BookDto> GetBookByISBNAsync(string isbn)
		{
			var book = _unitOfWork.Books.GetByISBNAsync(isbn);
			if (book == null)
				throw new DomainExceptions($"Book ISBN {isbn} not found.");

			return _mapper.Map<BookDto>(book);
		}

		public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(string author)
		{
			var books = _unitOfWork.Books.GetByAuthorAsync(author);

			return _mapper.Map<IEnumerable<BookDto>>(books);
		}

		public async Task<IEnumerable<BookDto>> GetBooksByTitleAsync(string title)
		{
			var books = _unitOfWork.Books.GetByTitleAsync(title);

			return _mapper.Map<IEnumerable<BookDto>>(books);
		}

		public async Task<BookDto> UpdateBookAsync(UpdateBookDto bookDto)
		{
			var book = await _unitOfWork.Books.GetByIdAsync(bookDto.Id);
			if (book == null)
				throw new DomainExceptions($"Book Id {bookDto.Id} not found.");

			//Verify if ISBN already exists
			if (book.ISBN != bookDto.ISBN)
			{
				var existingBook = await _unitOfWork.Books.GetByISBNAsync(bookDto.ISBN);
				if (existingBook != null && existingBook.Id != bookDto.Id)
					throw new DomainExceptions($"Book ISBN {bookDto.ISBN} already exists.");
			}

			//Update book data
			book.UpdateBookInfo(
				bookDto.Title,
				bookDto.Author,
				bookDto.ISBN,
				bookDto.PublicationYear,
				bookDto.Publisher,
				bookDto.TotalCopies,
				bookDto.Genre,
				bookDto.Description);

			await _unitOfWork.Books.UpdateAsync(book);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<BookDto>(book);
		}
	}
}