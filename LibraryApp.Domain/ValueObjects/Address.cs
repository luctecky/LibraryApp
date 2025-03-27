namespace LibraryApp.Domain.ValueObjects
{
	public class Address
	{
		public string Street { get; }
		public string Number { get; }
		public string Neighborhood { get; }
		public string Complement { get; }
		public string City { get; }
		public string State { get; }
		public string PostalCode { get; }

		public Address(string street, string number, string neighborhood, string complement, string city, string state, string postalCode)
		{
			Street = street;
			Number = number;
			Neighborhood = neighborhood;
			Complement = complement;
			City = city;
			State = state;
			PostalCode = postalCode;
		}

		//Value Objects are immutable, so we need to create a new instance of the object when we need to change it.

		public Address WithStreet(string newStreet)
		{
			return new Address(newStreet, Number, Neighborhood, Complement, City, State, PostalCode);
		}

		//Value Objects are compared by their properties, so we need to override the Equals method to compare the properties of the objects.

		public override bool Equals(object? obj)
		{
			if (obj is not Address other)
				return false;

			return Street == other.Street &&
				   Number == other.Number &&
				   Neighborhood == other.Neighborhood &&
				   Complement == other.Complement &&
				   City == other.City &&
				   State == other.State &&
				   PostalCode == other.PostalCode;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Street, Number, Neighborhood, Complement, City, State, PostalCode);
		}
	}
}