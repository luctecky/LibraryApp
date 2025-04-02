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

		public UserService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task ActivateUserAsync(Guid id)
		{
			var user = await _unitOfWork.Users.GetByIdAsync(id);

			if (user == null)
				throw new DomainExceptions($"User Id {id} not found.");

			user.ActiveUser();

			await _unitOfWork.Users.UpdateAsync(user);
			await _unitOfWork.SaveChangesAsync();
		}

		public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
		{
			//verify if user already exists
			var existingUserEmail = await _unitOfWork.Users.GetByEmailAsync(userDto.Email);

			if (existingUserEmail != null)
				throw new DomainExceptions($"User email {userDto.Email} already exists.");

			var existingDocumentId = await _unitOfWork.Users.GetByDocumentIdAsync(userDto.DocumentId);

			if (existingDocumentId != null)
				throw new DomainExceptions($"User DocumentId {userDto.DocumentId} already exists.");

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

			//Verify if user has Active Loans

			var activeLoans = await _unitOfWork.Loans.GetLoansByUserAsync(id);

			if (activeLoans.Any(loan => !loan.IsReturned))
				throw new DomainExceptions($"User Id {id} has active loans.");

			user.DeactivateUser();

			await _unitOfWork.Users.UpdateAsync(user);
			await _unitOfWork.SaveChangesAsync();
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

		public async Task<UserDto> GetUserByDocumentIdAsync(string documentId)
		{
			var user = await _unitOfWork.Users.GetByDocumentIdAsync(documentId);

			if (user == null)
				throw new DomainExceptions($"User DocumentId {documentId} not found.");

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
			var user = await _unitOfWork.Users.GetByIdAsync(userDto.Id);

			if (user == null)
				throw new DomainExceptions($"User Id {userDto.Id} not found.");

			//Verify if data has changed and if exists other user with the same data
			if (user.Email != userDto.Email)
			{
				var existingUserEmail = await _unitOfWork.Users.GetByEmailAsync(userDto.Email);

				if (existingUserEmail != null && existingUserEmail.Id != userDto.Id)
					throw new DomainExceptions($"User email {userDto.Email} already exists.");
			}

			//Verify if documentId has changed and if exists other user with the same documentId
			if (user.DocumentId != userDto.DocumentId)
			{
				var existingDocumentId = await _unitOfWork.Users.GetByDocumentIdAsync(userDto.DocumentId);

				if (existingDocumentId != null && existingDocumentId.Id != userDto.Id)
					throw new DomainExceptions($"User DocumentId {userDto.DocumentId} already exists.");
			}

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