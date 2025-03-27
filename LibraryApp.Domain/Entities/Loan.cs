using LibraryApp.Domain.Exceptions;

namespace LibraryApp.Domain.Entities
{
	public class Loan : BaseEntity
	{
		public Guid BookId { get; private set; }
		public Book Book { get; private set; }
		public Guid UserId { get; private set; }
		public User User { get; private set; }
		public DateTime LoanDate { get; private set; }
		public DateTime DueDate { get; private set; }
		public DateTime? ReturnDate { get; private set; }
		public bool IsReturned => ReturnDate.HasValue;

		//EF Core private Constructor
		private Loan()
		{
		}

		//Constructor to create a new loan
		public Loan(Book book, User user, int loanDurationInDays = 14)
		{
			if (book == null)
				throw new DomainExceptions("Book is required");

			if (user == null)
				throw new DomainExceptions("User is required");

			if (!user.IsActive)
				throw new DomainExceptions("User is not active");

			if (book.AvaliableCopies <= 0)
				throw new DomainExceptions("Book is not avaliable");

			Book = book;
			BookId = book.Id;

			User = user;
			UserId = user.Id;

			LoanDate = DateTime.UtcNow;
			DueDate = LoanDate.AddDays(loanDurationInDays);

			//Update the state of the book and user
			book.BorrowCopy();
			user.AddLoan(this);
		}

		public bool IsOverdue()
		{
			return !IsReturned && DateTime.UtcNow > DueDate;
		}

		public void ExtendLoan(int additionalDays)
		{
			if (IsReturned)
				throw new DomainExceptions("The book was already returned");

			if (IsOverdue())
				throw new DomainExceptions("The book is overdue");

			DueDate = DueDate.AddDays(additionalDays);
		}

		public void ReturnBook()
		{
			if (IsReturned)
				throw new DomainExceptions("The book was already returned");
			Book.ReturnCopy();
			ReturnDate = DateTime.UtcNow;
		}

		public int GetDaysOverdue()
		{
			if (!IsOverdue())
				return 0;

			return (int)(DateTime.UtcNow - DueDate).TotalDays;
		}
	}
}