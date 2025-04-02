using LibraryApp.Application.DTOs.Loan;

namespace LibraryApp.Application.Interfaces
{
	public interface ILoanService
	{
		Task<IEnumerable<LoanDto>> GetAllLoansAsync();

		Task<LoanDto> GetLoanByIdAsync(Guid id);

		Task<IEnumerable<LoanDto>> GetActiveAsync();

		Task<IEnumerable<LoanDto>> GetOverdueLoansAsync();

		Task<IEnumerable<LoanDto>> GetLoansByUserAsync(Guid userId);

		Task<IEnumerable<LoanDto>> GetLoansByBookAsync(Guid bookId);

		Task<LoanDto> CreateLoanAsync(CreateLoanDto loanDto);

		Task ExtendLoanAsync(Guid id, int additionalDays);

		Task ReturnBookAsync(Guid id);
	}
}