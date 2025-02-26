using LibraryAppBlazor.Domain.Entities;
using LibraryAppBlazor.Domain.Interfaces;

namespace LibraryAppBlazor.Application.Services
{
	public class LoanService : ILoanService
	{
		private readonly ILoanRepository _loanRepository;
		private readonly IBookRepository _bookRepository;

		public LoanService(ILoanRepository loanRepository, IBookRepository bookRepository)
		{
			_loanRepository = loanRepository;
			_bookRepository = bookRepository;
		}

		public async Task<decimal> CalculateFineAsync(Guid loanId, decimal dailyRate = 0.50M)
		{
			var loan = await _loanRepository.GetByIdAsync(loanId);
			if (loan == null)
				throw new Exception("Empréstimo não encontrado");

			return loan.CalculateFine(dailyRate);
		}

		public async Task<Loan> CreateLoanAsync(Guid bookId, string name, string email, int loanDays = 14)
		{
			//Get the book
			var book = await _bookRepository.GetByIdAsync(bookId);
			if (book == null)
				throw new Exception("Livro não encontrado");

			//Validate the book availability
			var activeLoans = await _loanRepository.GetLoansByBookIsAsync(bookId);
			if (activeLoans.Any(l => l.ReturnDate == null))
				throw new Exception("O Livro não está disponível");

			//Create the loan
			var loan = new Loan(book, name, email, DateTime.UtcNow, loanDays);
			await _loanRepository.AddAsync(loan);

			return loan;
		}

		public async Task ExtendLoanAsync(Guid loanId, int additionalDays)
		{
			var loan = await _loanRepository.GetByIdAsync(loanId);
			if (loan == null)
				throw new Exception($"Empréstimo não encontrado: {loanId}");

			loan.ExtendLoan(additionalDays);
			await _loanRepository.UpdateAsync(loan);
		}

		public async Task<IEnumerable<Loan>> GetActiveLoansdAsync()
		{
			return await _loanRepository.GetActiveLoansAsync();
		}

		public async Task<IEnumerable<Loan>> GetAllLoansAsync()
		{
			return await _loanRepository.GetAllAsync();
		}

		public async Task<Loan> GetLoanByIdAsync(Guid id)
		{
			return await _loanRepository.GetByIdAsync(id);
		}

		public async Task<IEnumerable<Loan>> GetLoansByBookAsync(Guid bookId)
		{
			return await _loanRepository.GetLoansByBookIsAsync(bookId);
		}

		public async Task<IEnumerable<Loan>> GetLoansByBorrowerAsync(string email)
		{
			return await _loanRepository.GetLoansByBorrowerEmailAsync(email);
		}

		public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
		{
			return await _loanRepository.GetOverdueLoansAsync();
		}

		public async Task ReturnBookAsync(Guid loanId)
		{
			var loan = await _loanRepository.GetByIdAsync(loanId);
			if (loan == null)
				throw new Exception($"Empréstimo não encontrado: {loanId}");

			loan.ReturnBook();
			await _loanRepository.UpdateAsync(loan);
		}
	}
}