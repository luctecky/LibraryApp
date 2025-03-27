using LibraryApp.Domain.Exceptions;

namespace LibraryApp.Domain.Entities
{
	public class User : BaseEntity
	{
		public string Name { get; private set; }
		public string Email { get; private set; }
		public string DocumentId { get; private set; }
		public string Phone { get; private set; }
		public string Address { get; private set; }
		public DateTime RegistrationDate { get; private set; }
		public bool IsActive { get; private set; }

		private readonly List<Loan> _loans = new();

		//EF Core private Constructor
		private User()
		{
		}

		public User(string name, string email, string documentId, string phone, string address)
		{
			ValidateUserData(name, email, documentId);
			Name = name;
			Email = email;
			DocumentId = documentId;
			Phone = phone;
			Address = address;
			RegistrationDate = DateTime.UtcNow;
			IsActive = true;
		}

		//Method to validate the data of the user
		private void ValidateUserData(string name, string email, string documentId)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new DomainExceptions("Name is required");

			if (string.IsNullOrWhiteSpace(email))
				throw new DomainExceptions("Email is required");

			if (string.IsNullOrWhiteSpace(documentId))
				throw new DomainExceptions("DocumentId is required");
		}

		//Method to alter the state of the user
		public void UpdateUserInfo(string name, string email, string documentId, string phone, string address)
		{
			ValidateUserData(name, email, documentId);

			Name = name;
			Email = email;
			DocumentId = documentId;
			Phone = phone;
			Address = address;
		}

		public void ActiveUser()
		{
			IsActive = true;
		}

		public void DeactivateUser()
		{
			IsActive = false;
		}

		public bool HasActiveLoan()
		{
			return _loans.Any(loan => !loan.IsReturned);
		}

		public bool HasOverdueLoan()
		{
			return _loans.Any(loan => loan.IsOverdue());
		}

		public void AddLoan(Loan loan)
		{
			if (!IsActive)
				throw new DomainExceptions("User is not active");

			_loans.Add(loan);
		}
	}
}