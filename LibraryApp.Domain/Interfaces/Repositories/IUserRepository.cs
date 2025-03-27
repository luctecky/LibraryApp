using LibraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Interfaces.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User> GetByEmailAsync(string email);

		Task<User> GetByDocumentIdAsync(string documentId);

		Task<IEnumerable<User>> GetActiveUsersAsync();

		Task<IEnumerable<User>> GetUserWithOverdueLoans();
	}
}