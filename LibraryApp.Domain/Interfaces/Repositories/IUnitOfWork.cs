using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Interfaces.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		IBookRepository Books { get; }
		ILoanRepository Loans { get; }
		IUserRepository Users { get; }

		Task<int> SaveChangesAsync();

		Task BeginTransactionAsync();

		Task CommitTransactionAsync();

		Task RollbackTransactionAsync();
	}
}