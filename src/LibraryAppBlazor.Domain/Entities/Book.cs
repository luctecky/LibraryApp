namespace LibraryAppBlazor.Domain.Entities
{
	public class Book
	{
		// Properties represent state of the entity
		public Guid Id { get; private set; }

		public string Title { get; private set; }
		public string Author { get; private set; }
		public string ISBN { get; private set; }
		public int PublicationYear { get; private set; }
		public DateTime CreatedAt { get; private set; }

		//protected constructor to EF Core

		public Book()
		{
		}

		// Constructor to create a new book
		public Book(string title, string author, string isbn, int publicationYear)
		{
			Id = Guid.NewGuid();
			SetTitle(title);
			SetAuthor(author);
			SetISBN(isbn);
			SetPublicationYear(publicationYear);
			CreatedAt = DateTime.UtcNow;
		}

		//Using private setters to ensure that the properties can only be set from within the class
		private void SetPublicationYear(int year)
		{
			if (year < 1400 || year > DateTime.UtcNow.Year)
			{
				throw new ArgumentException("Invalid publication year");
			}

			PublicationYear = year;
		}

		private void SetISBN(string isbn)
		{
			if (string.IsNullOrWhiteSpace(isbn))
			{
				throw new ArgumentException("ISBN cannot be empty");
			}

			ISBN = isbn;
		}

		private void SetAuthor(string author)
		{
			if (string.IsNullOrWhiteSpace(author))
			{
				throw new ArgumentException("Author cannot be empty");
			}

			Author = author;
		}

		private void SetTitle(string title)
		{
			if (string.IsNullOrWhiteSpace(title))
			{
				throw new ArgumentException("Title cannot be empty");
			}

			Title = title;
		}
	}
}