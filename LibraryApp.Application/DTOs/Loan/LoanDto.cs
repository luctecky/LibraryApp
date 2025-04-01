using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.DTOs.Loan
{
	public class LoanDto
	{
		public Guid Id { get; set; }
		public Guid BookId { get; set; }
		public string BookTitle { get; set; }
		public string BookAuthor { get; set; }
		public string BookISBN { get; set; }
		public int BookPublicationYear { get; set; }
		public string BookPublisher { get; set; }
		public int BookAvaliableCopies { get; set; }
		public int BookTotalCopies { get; set; }
		public string BookGenre { get; set; }
		public string BookDescription { get; set; }
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public DateTime LoanDate { get; set; }
		public DateTime ReturnDate { get; set; }
		public bool Returned { get; set; }
	}
}