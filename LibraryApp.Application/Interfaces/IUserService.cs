using LibraryApp.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Interfaces
{
	public interface IUserService
	{
		Task<IEnumerable<UserDto>> GetAllUsersAsync();

		Task<UserDto> GetUserByIdAsync(Guid id);

		Task<UserDto> GetUserByEmailAsync(string email);

		Task<UserDto> GetUserByDocumentIdAsync(string userName);

		Task<UserDto> CreateUserAsync(CreateUserDto userDto);

		Task<UserDto> UpdateUserAsync(UpdateUserDto userDto);

		Task DeleteUserAsync(Guid id);

		Task ActivateUserAsync(Guid id);

		Task DeactivateUserAsync(Guid id);
	}
}