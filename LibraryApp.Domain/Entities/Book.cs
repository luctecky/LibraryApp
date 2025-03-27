using LibraryApp.Domain.Exceptions;

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

	//Method to validate the data of the book
	private void ValidateBookData(string title, string author, string isbn, int publicationYear, string publisher, int totalCopies)
	{
		if (string.IsNullOrWhiteSpace(title))
			throw new DomainExceptions("Title is required");

		if (string.IsNullOrWhiteSpace(author))
			throw new DomainExceptions("Author is required");

		if (string.IsNullOrWhiteSpace(isbn))
			throw new DomainExceptions("ISBN is required");

		if (publicationYear <= 0)
			throw new DomainExceptions("Publication year must be positive");

		if (string.IsNullOrWhiteSpace(publisher))
			throw new DomainExceptions("Publisher is required");

		if (totalCopies <= 0)
			throw new DomainExceptions("Total copie must be positive");
	}

	//Method to alter the state of the book

	public void UpdateAvailableCopies(int newAvailableCopies)
	{
		if (newAvailableCopies < 0)
			throw new DomainExceptions("Available copies must be positive");

		if (newAvailableCopies > TotalCopies)
			throw new DomainExceptions("Available copies can't be greater than total copies");

		AvaliableCopies = newAvailableCopies;
	}

	public void BorrowCopy()
	{
		if (AvaliableCopies <= 0)
			throw new DomainExceptions("There are no available copies of this book");

		AvaliableCopies--;
	}

	public void ReturnCopy()
	{
		if (AvaliableCopies >= TotalCopies)
			throw new DomainExceptions("All copies of this book are available");

		AvaliableCopies++;
	}

	public void UpdateBookInfo(string title, string author, string isbn, int publicationYear, string publisher, int totalCopies, string genre, string description)
	{
		ValidateBookData(title, author, isbn, publicationYear, publisher, totalCopies);

		Title = title;
		Author = author;
		ISBN = isbn;
		PublicationYear = publicationYear;
		Publisher = publisher;

		//If TotalCopies is less than the current AvaliableCopies, we need to update the AvaliableCopies to the new TotalCopies
		if (totalCopies > TotalCopies)
		{
			int newCopies = totalCopies - TotalCopies;
			AvaliableCopies += newCopies;
		}
		//If TotalCopies decreasesm we need to update the AvaliableCopies to the new TotalCopies
		else if (totalCopies < TotalCopies)
		{
			int removedCopies = TotalCopies - totalCopies;
			AvaliableCopies = Math.Max(0, AvaliableCopies - removedCopies);
		}

		TotalCopies = totalCopies;
		Genre = genre;
		Description = description;
	}
}