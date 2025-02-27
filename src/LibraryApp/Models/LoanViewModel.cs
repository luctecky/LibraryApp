using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
	public class LoanViewModel
	{
		public Guid Id { get; set; }

		public Guid BookId { get; set; }

		public string BookTitle { get; set; }

		[Required(ErrorMessage = "O nome do solicitante é obrigatório")]
		[StringLength(200, ErrorMessage = "O nome do solicitante deve ter no máximo 200 caracteres")]
		public string BorrowerName { get; set; }

		[Required(ErrorMessage = "O email do solicitante é obrigatório")]
		[EmailAddress(ErrorMessage = "O email do solicitante é inválido")]
		[StringLength(200, ErrorMessage = "O email deve ter no máximo 200 caracteres")]
		public string BorrowerEmail { get; set; }

		public DateTime LoanDate { get; set; }

		public DateTime DueDate { get; set; }

		public DateTime? ReturnDate { get; set; }

		[Range(1, 60, ErrorMessage = "O prazo de devolução deve ser entre 1 e 60 dias")]
		public int LoanDays { get; set; } = 14;

		public bool IsReturned => ReturnDate.HasValue;

		public bool IsOverdue => !IsReturned && DueDate < DateTime.Now;

		public decimal Fine { get; set; }
	}
}