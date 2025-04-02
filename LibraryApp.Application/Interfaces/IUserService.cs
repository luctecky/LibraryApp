using LibraryApp.Application.DTOs.User;

namespace LibraryApp.Application.Interfaces
{
	public interface IUserService
	{
		Task<IEnumerable<UserDto>> GetAllUsersAsync();

		Task<UserDto> GetUserByIdAsync(Guid id);

		Task<UserDto> GetUserByEmailAsync(string email);

		Task<UserDto> GetUserByDocumentIdAsync(string documentId);

		Task<UserDto> CreateUserAsync(CreateUserDto userDto);

		Task<UserDto> UpdateUserAsync(UpdateUserDto userDto);

		Task DeleteUserAsync(Guid id);

		Task ActivateUserAsync(Guid id);

		Task DeactivateUserAsync(Guid id);
	}
}