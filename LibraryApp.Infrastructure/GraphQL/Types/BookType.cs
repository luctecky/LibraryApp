using GraphQL.Types;
using LibraryApp.Domain.Entities;

namespace LibraryApp.Infrastructure.GraphQL.Types
{
	public class BookType : ObjectGraphType<Book>
	{
		public BookType()
		{
			Field(x => x.Id, type: typeof(IdGraphType)).Description("ID do Livro");
			Field(x => x.Title).Description("Título do Livro");
			Field(x => x.Author).Description("Autor do Livro");
			Field(x => x.ISBN).Description("ISBN do livro");
			Field(x => x.PublicationYear).Description("Ano de publicação");
			Field(x => x.Publisher).Description("Editora");
			Field(x => x.AvaliableCopies).Description("Exemplares disponíveis");
			Field(x => x.TotalCopies).Description("Total de exemplares");
			Field(x => x.Genre).Description("Gênero do livro");
			Field(x => x.Description, nullable: true).Description("Descrição do livro");
			Field(x => x.CreatedAt).Description("Data de criação");
			Field(x => x.UpdatedAt).Description("Data da ultima atualização");
		}
	}
}