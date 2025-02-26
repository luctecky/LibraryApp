using LibraryAppBlazor.Domain.Entities;
using LibraryAppBlazor.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppBlazor.Infrastructure.Data.Repositories
{
	public class LoanRepository : ILoanRepository
	{
		private readonly LibraryAppDbContext _context;

		public LoanRepository(LibraryAppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Loan loan)
		{
			await _context.Loans.AddAsync(loan);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var loan = await GetByIdAsync(id);
			if (loan != null)
			{
				_context.Loans.Remove(loan);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
		{
			return await _context.Loans
				.Include(l => l.Book)
				.Where(l => l.ReturnDate == null)
				.ToListAsync();
		}

		public async Task<IEnumerable<Loan>> GetAllAsync()
		{
			return await _context.Loans
				.Include(l => l.Book)
				.ToListAsync();
		}

		public async Task<Loan> GetByIdAsync(Guid id)
		{
			return await _context.Loans
				.Include(l => l.Book)
				.FirstOrDefaultAsync(l => l.Id == id);
		}

		public async Task<IEnumerable<Loan>> GetLoansByBookIsAsync(Guid bookId)
		{
			return await _context.Loans
				.Include(l => l.Book)
				.Where(l => l.BookId == bookId)
				.ToListAsync();
		}

		public async Task<IEnumerable<Loan>> GetLoansByBorrowerEmailAsync(string email)
		{
			return await _context.Loans
			   .Include(l => l.Book)
			   .Where(l => l.BorrowerEmail.ToLower() == email.ToLower())
			   .ToListAsync();
		}

		public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
		{
			var today = DateTime.UtcNow.Date;
			return await _context.Loans
				.Include(l => l.Book)
				.Where(l => l.ReturnDate == null && l.DueDate.Date < today)
				.ToListAsync();
		}

		public async Task UpdateAsync(Loan loan)
		{
			_context.Entry(loan).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}
	}
}