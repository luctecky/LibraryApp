using LibraryAppBlazor.Domain.Entities;

namespace LibraryAppBlazor.Application.Services
{
	public interface ILoanService
	{
		//Basic Operations
		Task<IEnumerable<Loan>> GetAllLoansAsync();

		Task<Loan> GetLoanByIdAsync(Guid id);

		//Epecific Operations
		Task<Loan> CreateLoanAsync(Guid bookId, string name, string email, int loanDays = 14);

		Task ReturnBookAsync(Guid loanId);

		Task ExtendLoanAsync(Guid loanId, int additionalDays);

		//Espcific Queries
		Task<IEnumerable<Loan>> GetActiveLoansAsync();

		Task<IEnumerable<Loan>> GetOverdueLoansAsync();

		Task<IEnumerable<Loan>> GetLoansByBookAsync(Guid bookId);

		Task<IEnumerable<Loan>> GetLoansByBorrowerAsync(string email);

		//Fine Calculation
		Task<decimal> CalculateFineAsync(Guid loanId, decimal dailyRate = 0.50m);
	}
}