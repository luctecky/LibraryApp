using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.DTOs.User
{
	public class UserDto
	{
		public Guid UserID { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string DocumentId { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public DateTime RegistrationDate { get; set; }
		public bool IsActive { get; set; }
	}
}