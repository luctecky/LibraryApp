﻿namespace LibraryApp.Application.DTOs.User
{
	public class UpdateUserDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string DocumentId { get; set; }
		public string Phone { get; set; }
		public string Address { get; set; }
		public bool IsActive { get; set; }
	}
}