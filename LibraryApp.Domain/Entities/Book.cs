namespace LibraryApp.Domain.Entities;

public class Book : BaseEntity
{
	public string Title { get; private set; }
	public string Author { get; private set; }
	public string ISBN { get; private set; }
	public int PublicationYear { get; private set; }
	public string Publisher { get; private set; }
	public int AvaliableCopies { get; private set; }
	public int TotalCopies { get; private set; }
	public string Genre { get; private set; }
	public string Description { get; set; }

	//EF Core private Constructor
	private Book()
	{
	}

	public Book(string title, string author, string isbn, int publicationYear, string publisher, int totalCopies, string genre, string description)
	{
		ValidateBookData(title, author, isbn, publicationYear, publisher, totalCopies);

		Title = title;
		Author = author;
		ISBN = isbn;
		PublicationYear = publicationYear;
		Publisher = publisher;
		AvaliableCopies = totalCopies;
		TotalCopies = totalCopies;
		Genre = genre;
		Description = description;
	}

	private void ValidateBookData(string title, string author, string isbn, int publicationYear, string publisher, int totalCopies)
	{
		throw new NotImplementedException();
	}
}