using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces.Repositories;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories
{
	public class LoanRepository : Repository<Loan>, ILoanRepository
	{
		public LoanRepository(ApplicationDbContext context) : base(context)
		{
		}

		public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
		{
			return await _dbSet
				.Include(loan => loan.Book)
				.Include(loan => loan.User)
				.Where(loan => loan.ReturnDate == null)
				.ToListAsync();
		}

		public async Task<IEnumerable<Loan>> GetLoansByBookAsync(Guid bookId)
		{
			return await _dbSet
				.Include(loan => loan.User)
				.Where(loan => loan.BookId == bookId)
				.ToListAsync();
		}

		public async Task<IEnumerable<Loan>> GetLoansByUserAsync(Guid userId)
		{
			return await _dbSet
				.Include(loan => loan.Book)
				.Where(loan => loan.UserId == userId)
				.ToListAsync();
		}

		public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
		{
			var today = DateTime.Now;

			return await _dbSet
				.Include(loan => loan.Book)
				.Include(loan => loan.User)
				.Where(loan => loan.ReturnDate == null && loan.DueDate < today)
				.ToListAsync();
		}
	}
}