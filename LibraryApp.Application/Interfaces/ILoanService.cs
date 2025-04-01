using LibraryApp.Application.DTOs.Loan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Interfaces
{
	public interface ILoanService
	{
		Task<IEnumerable<LoanDto>> GetAllLoansAsync();

		Task<LoanDto> GetLoanByIdAsync(Guid id);

		Task<IEnumerable<LoanDto>> GetActiveAsync(string status);

		Task<IEnumerable<LoanDto>> GetOverdueLoansAsync(string status);

		Task<IEnumerable<LoanDto>> GetLoansByUserAsync(Guid userId);

		Task<IEnumerable<LoanDto>> GetLoansByBookAsync(Guid bookId);

		Task<LoanDto> CreateLoanAsync(CreateLoanDto loanDto);

		Task ExtendLoanAsync(Guid id, int additionalDays);

		Task ReturnLoanAsync(Guid id);
	}
}