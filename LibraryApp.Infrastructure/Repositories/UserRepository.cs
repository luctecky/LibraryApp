using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces.Repositories;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Infrastructure.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		public UserRepository(ApplicationDbContext context) : base(context)
		{
		}

		public async Task<IEnumerable<User>> GetActiveUsersAsync()
		{
			return await _dbSet.Where(user => user.IsActive).ToListAsync();
		}

		public async Task<User> GetByDocumentIdAsync(string documentId)
		{
			return await _dbSet.FirstOrDefaultAsync(user => user.DocumentId == documentId);
		}

		public async Task<User> GetByEmailAsync(string email)
		{
			return await _dbSet.FirstOrDefaultAsync(user => user.Email == email);
		}

		public async Task<IEnumerable<User>> GetUserWithOverdueLoans()
		{
			var today = DateTime.Now;

			return await _context.Users
				.Where(user => user.IsActive && _context.Loans
				.Any(loan => loan.UserId == user.Id && loan.ReturnDate == null && loan.DueDate < today))
				.ToListAsync();
		}
	}
}