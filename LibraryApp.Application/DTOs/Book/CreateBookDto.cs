using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Application.DTOs.Book
{
	public class CreateBookDto
	{
		[Required(ErrorMessage = "O Título é obrigatório")]
		[StringLength(200, ErrorMessage = "O Título deve ter no máximo 200 caracteres")]
		public string Title { get; set; }

		[Required(ErrorMessage = "O Autor é obrigatório")]
		[StringLength(200, ErrorMessage = "O Autor deve ter no máximo 200 caracteres")]
		public string Author { get; set; }

		[Required(ErrorMessage = "O ISBN é obrigatório")]
		[StringLength(20, ErrorMessage = "O ISBN deve ter no máximo 20 caracteres")]
		public string ISBN { get; set; }

		[Required(ErrorMessage = "O Ano de Publicação é obrigatório")]
		[Range(1000, 9999, ErrorMessage = "O Ano de Publicação deve ter 4 dígitos")]
		public int PublicationYear { get; set; }

		[Required(ErrorMessage = "A Editora é obrigatória")]
		[StringLength(200, ErrorMessage = "A Editora deve ter no máximo 200 caracteres")]
		public string Publisher { get; set; }

		[Required(ErrorMessage = "O Número de Cópias é obrigatório")]
		[Range(1, int.MaxValue, ErrorMessage = "O Número de Cópias deve ser maior que 0")]
		public int TotalCopies { get; set; }

		[Required(ErrorMessage = "O Gênero é obrigatório")]
		[StringLength(200, ErrorMessage = "O Gênero deve ter no máximo 200 caracteres")]
		public string Genre { get; set; }

		[Required(ErrorMessage = "A Descrição é obrigatória")]
		[StringLength(1000, ErrorMessage = "A Descrição deve ter no máximo 1000 caracteres")]
		public string Description { get; set; }
	}
}