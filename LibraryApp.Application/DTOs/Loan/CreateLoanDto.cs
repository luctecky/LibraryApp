using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.DTOs.Loan
{
	public class CreateLoanDto
	{
		public Guid BookId { get; set; }
		public Guid UserId { get; set; }
		public DateTime LoanDate { get; set; }
		public DateTime ReturnDate { get; set; }
		public bool Returned { get; set; }
	}
}