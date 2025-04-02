using AutoMapper;
using LibraryApp.Application.DTOs.User;
using LibraryApp.Application.Interfaces;
using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Exceptions;
using LibraryApp.Domain.Interfaces.Repositories;

namespace LibraryApp.Application.Services
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public async Task ActivateUserAsync(Guid id)
		{
			var user = await _unitOfWork.Users.GetByIdAsync(id);

			if (user == null)
				throw new DomainExceptions($"User Id {id} not found.");

			user.ActiveUser();

			await _unitOfWork.SaveChangesAsync();
			await _unitOfWork.CommitTransactionAsync();
		}

		public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
		{
			//verify if user already exists
			var existingUser = await _unitOfWork.Users.GetByEmailAsync(userDto.Email);

			if (existingUser != null)
				throw new DomainExceptions($"User email {userDto.Email} already exists.");

			//Create a instance of user
			var user = _mapper.Map<User>(userDto);

			//Add user to the repository
			await _unitOfWork.Users.AddAsync(user);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<UserDto>(user);
		}

		public async Task DeactivateUserAsync(Guid id)
		{
			var user = await _unitOfWork.Users.GetByIdAsync(id);

			if (user == null)
				throw new DomainExceptions($"User Id {id} not found.");

			if (!user.IsActive)
				throw new DomainExceptions($"User Id {id} is already inactive.");

			user.DeactivateUser();

			await _unitOfWork.SaveChangesAsync();
			await _unitOfWork.CommitTransactionAsync();
		}

		public async Task DeleteUserAsync(Guid id)
		{
			var user = await _unitOfWork.Users.GetByIdAsync(id);

			if (user == null)
				throw new DomainExceptions($"User Id {id} not found.");

			//Verify if user has Active Loans
			var activeLoans = await _unitOfWork.Loans.GetLoansByUserAsync(id);

			if (activeLoans.Any(loans => !loans.IsReturned))
				throw new DomainExceptions($"User Id {id} has active loans.");

			await _unitOfWork.Users.DeleteAsync(user);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
		{
			var users = await _unitOfWork.Users.GetAllAsync();

			if (users == null)
				throw new DomainExceptions("No users found.");

			return _mapper.Map<IEnumerable<UserDto>>(users);
		}

		public async Task<UserDto> GetUserByDocumentIdAsync(string DocumentId)
		{
			var user = await _unitOfWork.Users.GetByDocumentIdAsync(DocumentId);

			if (user == null)
				throw new DomainExceptions($"User DocumentId {DocumentId} not found.");

			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> GetUserByEmailAsync(string email)
		{
			var user = await _unitOfWork.Users.GetByEmailAsync(email);

			if (user == null)
				throw new DomainExceptions($"User email {email} not found.");

			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> GetUserByIdAsync(Guid id)
		{
			var user = await _unitOfWork.Users.GetByIdAsync(id);

			if (user == null)
				throw new DomainExceptions($"User Id {id} not found.");

			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> UpdateUserAsync(UpdateUserDto userDto)
		{
			var user = await _unitOfWork.Users.GetByEmailAsync(userDto.Email);

			if (user == null)
				throw new DomainExceptions($"User email {userDto.Email} not found.");

			user.UpdateUserInfo(
				userDto.Name,
				userDto.Email,
				userDto.DocumentId,
				userDto.Phone,
				userDto.Address);

			await _unitOfWork.Users.UpdateAsync(user);
			await _unitOfWork.SaveChangesAsync();

			return _mapper.Map<UserDto>(user);
		}
	}
}