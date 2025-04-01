namespace LibraryApp.Application.DTOs.Book
{
	public class UpdateBookDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public string ISBN { get; set; }
		public int PublicationYear { get; set; }
		public string Publisher { get; set; }
		public int TotalCopies { get; set; }
		public string Genre { get; set; }
		public string Description { get; set; }
	}
}