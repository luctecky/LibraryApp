using AutoMapper;
using LibraryApp.Application.DTOs.Loan;
using LibraryApp.Application.Interfaces;
using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Exceptions;
using LibraryApp.Domain.Interfaces.Repositories;

namespace LibraryApp.Application.Services
{
	public class LoanService : ILoanService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public async Task<LoanDto> CreateLoanAsync(CreateLoanDto loanDto)
		{
			//verify if book is available
			var avaliableBook = await _unitOfWork.Loans.GetActiveLoansAsync();

			if (avaliableBook.Any(book => book.BookId == loanDto.BookId && book.IsReturned == false))
				throw new DomainExceptions($"Book Id {loanDto.BookId} is not available.");

			//verify if user has active loans
			var activeLoans = await _unitOfWork.Loans.GetLoansByUserAsync(loanDto.UserId);
			if (activeLoans.Any(loans => !loans.IsReturned))
				throw new DomainExceptions($"User Id {loanDto.UserId} has active loans.");

			//Create a instance of loan
			var loan = _mapper.Map<Loan>(loanDto);

			//Add loan to the repository
			await _unitOfWork.Loans.AddAsync(loan);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<LoanDto>(loan);
		}

		public async Task ExtendLoanAsync(Guid id, int additionalDays)
		{
			var loan = await _unitOfWork.Loans.GetByIdAsync(id);

			if (loan == null)
				throw new DomainExceptions($"Loan Id {id} not found.");

			if (loan.IsReturned)
				throw new DomainExceptions($"Loan Id {id} was already returned.");

			loan.ExtendLoan(additionalDays);

			await _unitOfWork.Loans.UpdateAsync(loan);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<IEnumerable<LoanDto>> GetActiveAsync()
		{
			var loans = await _unitOfWork.Loans.GetActiveLoansAsync();

			if (loans == null)
				throw new DomainExceptions("No active loans found.");

			return _mapper.Map<IEnumerable<LoanDto>>(loans);
		}

		public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
		{
			var loans = await _unitOfWork.Loans.GetAllAsync();

			if (loans == null)
				throw new DomainExceptions("No loans found.");

			return _mapper.Map<IEnumerable<LoanDto>>(loans);
		}

		public async Task<LoanDto> GetLoanByIdAsync(Guid id)
		{
			var loan = await _unitOfWork.Loans.GetByIdAsync(id);

			if (loan == null)
				throw new DomainExceptions($"Loan Id {id} not found.");

			return _mapper.Map<LoanDto>(loan);
		}

		public async Task<IEnumerable<LoanDto>> GetLoansByBookAsync(Guid bookId)
		{
			var loans = await _unitOfWork.Loans.GetLoansByBookAsync(bookId);

			if (loans == null)
				throw new DomainExceptions($"No loans found for Book Id {bookId}.");

			return _mapper.Map<IEnumerable<LoanDto>>(loans);
		}

		public async Task<IEnumerable<LoanDto>> GetLoansByUserAsync(Guid userId)
		{
			var loans = await _unitOfWork.Loans.GetLoansByUserAsync(userId);

			if (loans == null)
				throw new DomainExceptions($"No loans found for User Id {userId}.");

			return _mapper.Map<IEnumerable<LoanDto>>(loans);
		}

		public async Task<IEnumerable<LoanDto>> GetOverdueLoansAsync()
		{
			var overdueLoans = await _unitOfWork.Loans.GetOverdueLoansAsync();

			if (overdueLoans == null)
				throw new DomainExceptions("No overdue loans found.");

			return _mapper.Map<IEnumerable<LoanDto>>(overdueLoans);
		}

		public async Task ReturnLoanAsync(Guid id)
		{
			var loan = await _unitOfWork.Loans.GetByIdAsync(id);

			if (loan == null)
				throw new DomainExceptions($"Loan Id {id} not found.");

			if (loan.IsReturned)
				throw new DomainExceptions($"Loan Id {id} was already returned.");

			loan.ReturnBook();

			await _unitOfWork.Loans.UpdateAsync(loan);
			await _unitOfWork.SaveChangesAsync();
		}
	}
}