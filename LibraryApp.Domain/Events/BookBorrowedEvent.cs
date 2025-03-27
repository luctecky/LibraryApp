using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Domain.Events
{
	public class BookBorrowedEvent : DomainEvent
	{
		public Guid BookId { get; }
		public Guid UserId { get; }
		public DateTime DueDate { get; }

		public BookBorrowedEvent(Guid bookId, Guid userId, DateTime dueDate) : base()
		{
			BookId = bookId;
			UserId = userId;
			DueDate = dueDate;
		}
	}
}