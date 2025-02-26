namespace LibraryAppBlazor.Domain.Entities
{
	public class Loan
	{
		//Properties representing the state of the Loan entity
		public Guid Id { get; private set; }

		//Relationship with the Book entity

		public Guid BookId { get; private set; }
		public Book Book { get; private set; }

		public string BorrowerName { get; private set; }
		public string BorrowerEmail { get; private set; }
		public DateTime LoanDate { get; private set; }
		public DateTime DueDate { get; private set; }
		public DateTime? ReturnDate { get; private set; }
		public bool IsReturned => ReturnDate.HasValue;
		public bool IsOverdue => !IsReturned && DueDate < DateTime.UtcNow;

		//Protected constructor for EF Core
		protected Loan()
		{
		}

		//Constructor to create a new Loan
		public Loan(Book book, string borrowerName, string borrowerEmail, DateTime loanDate, int loanDays = 14)
		{
			Id = Guid.NewGuid();
			SetBook(book);
			SetBorrower(borrowerName, borrowerEmail);
			LoanDate = DateTime.UtcNow;
			DueDate = LoanDate.AddDays(loanDays);
		}

		private void SetBorrower(string name, string email)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name), "Borrower name cannot be null or empty");

			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentNullException(nameof(email), "Borrower email cannot be null or empty");

			//Simple email validation
			if (!email.Contains("@") || !email.Contains("."))
				throw new ArgumentException("Invalid email address", nameof(email));

			BorrowerName = name;
			BorrowerEmail = email;
		}

		private void SetBook(Book book)
		{
			if (book == null)
				throw new ArgumentNullException(nameof(book), "Book cannot be null");

			Book = book;
			BookId = book.Id;
		}

		//Method to return a book
		public void ReturnBook()
		{
			if (IsReturned)
				throw new InvalidOperationException("Book is already returned");

			ReturnDate = DateTime.UtcNow;
		}

		//Method to extend the loan period
		public void ExtendLoan(int additionalDays)
		{
			if (IsReturned)
				throw new InvalidOperationException("Cannot extend a returned book");

			if (additionalDays <= 0)
				throw new ArgumentException("Additional days must be greater than zero", nameof(additionalDays));

			DueDate = DueDate.AddDays(additionalDays);
		}

		//Method to calculate fine for overdue books
		public decimal CalculateFine(decimal dailyRate = 0.50m)
		{
			if (!IsOverdue)
				return 0;

			DateTime endDate = ReturnDate ?? DateTime.UtcNow;
			int daysLate = (int)(endDate - DueDate).TotalDays;

			return Math.Max(0, daysLate) * dailyRate;
		}
	}
}