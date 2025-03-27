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

		public bool IsReturned { get; internal set; }

		internal bool IsOverdue()
		{
			throw new NotImplementedException();
		}
	}
}