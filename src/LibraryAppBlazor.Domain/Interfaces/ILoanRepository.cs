using LibraryAppBlazor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAppBlazor.Domain.Interfaces
{
	public interface ILoanRepository
	{
		//Basic CRUD operations
		Task<Loan> GetByIdAsync(Guid id);

		Task<IEnumerable<Loan>> GetAllAsync();

		Task<IEnumerable<Loan>> GetActiveLoansAsync();

		Task<IEnumerable<Loan>> GetOverdueLoansAsync();

		Task<IEnumerable<Loan>> GetLoansByBookIsAsync(Guid bookId);

		Task<IEnumerable<Loan>> GetLoansByBorrowerAsync(string borrowerName);

		Task AddAsync(Loan loan);

		Task UpdateAsync(Loan loan);

		Task DeleteAsync(Guid id);
	}
}