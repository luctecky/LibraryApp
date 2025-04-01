using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.DTOs.User
{
	public class CreateUserDto
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string DocumentId { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
	}
}