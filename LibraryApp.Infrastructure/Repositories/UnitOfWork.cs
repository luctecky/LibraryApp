using LibraryApp.Domain.Interfaces.Repositories;
using LibraryApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryApp.Infrastructure.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		private IDbContextTransaction _transaction;

		private IBookRepository _bookRepository;
		private ILoanRepository _loanRepository;
		private IUserRepository _userRepository;
		private bool _disposed;

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}

		public IBookRepository Books => _bookRepository ??= new BookRepository(_context);
		public ILoanRepository Loans => _loanRepository ??= new LoanRepository(_context);
		public IUserRepository Users => _userRepository ??= new UserRepository(_context);

		public async Task BeginTransactionAsync()
		{
			_transaction = await _context.Database.BeginTransactionAsync();
		}

		public async Task CommitTransactionAsync()
		{
			try
			{
				await _transaction?.CommitAsync();
			}
			finally
			{
				await _transaction?.RollbackAsync();
				_transaction = null;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_transaction?.Dispose();
					_context.Dispose();
				}
				_disposed = true;
			}
		}

		public async Task RollbackTransactionAsync()
		{
			try
			{
				await _transaction?.RollbackAsync();
			}
			finally
			{
				await _transaction?.RollbackAsync();
				_transaction = null;
			}
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}