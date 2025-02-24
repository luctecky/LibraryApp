using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
	public class BookViewModel
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "O título é obrigatório")]
		[StringLength(200, ErrorMessage = "O título deve ter no máximo duzendo caracteres")]
		public string Title { get; set; }

		[Required(ErrorMessage = "O autor é obrigatório")]
		[StringLength(200, ErrorMessage = "O autor deve ter no máximo duzendo caracteres")]
		public string Author { get; set; }

		[Required(ErrorMessage = "O ISBN é obrigatório")]
		[StringLength(13, ErrorMessage = "O ISBN deve ter no máximo treze caracteres")]
		public string ISBN { get; set; }

		[Required(ErrorMessage = "O ano de publicação é obrigatório")]
		[Range(1400, 9999, ErrorMessage = "O ano de publicação deve ser entre 1400 e 9999")]
		public int PublicationYear { get; set; }
	}
}