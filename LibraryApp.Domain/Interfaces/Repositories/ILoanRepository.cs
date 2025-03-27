using LibraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Interfaces.Repositories
{
	public interface ILoanRepository : IRepository<Loan>
	{
		Task<IEnumerable<Loan>> GetLoansByUserAsync(Guid userId);

		Task<IEnumerable<Loan>> GetLoansByBookAsync(Guid bookId);

		Task<IEnumerable<Loan>> GetActiveLoansAsync();

		Task<IEnumerable<Loan>> GetOverdueLoansAsync();
	}
}