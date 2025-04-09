using GraphQL;
using GraphQL.Types;
using LibraryApp.Domain.Interfaces.Repositories;
using LibraryApp.Infrastructure.GraphQL.Types;

namespace LibraryApp.Infrastructure.GraphQL.Queries
{
	public class BookQuery : ObjectGraphType
	{
		public BookQuery(IBookRepository bookRepository)
		{
			Field<BookType>("books")
				.Description("Lista de todos os livros")
				.ResolveAsync(async context => await bookRepository.GetAllAsync()
			);

			Field<BookType>("book")
				.Description("Busca o livro pelo ID")
				.Argument<NonNullGraphType<IdGraphType>>("id", "ID do livro")
				.ResolveAsync(async context => await bookRepository.GetByIdAsync(context.GetArgument<Guid>("id"))
			);

			Field<BookType>("bookByISBN")
				.Description("Busca o livro pelo ISBN")
				.Argument<NonNullGraphType<StringGraphType>>("isbn", "ISBN do livro")
				.ResolveAsync(async context => await bookRepository.GetByISBNAsync(context.GetArgument<string>("isbn"))
			);

			Field<ListGraphType<BookType>>("booksByTitle")
				.Description("Busca os livros pelo título")
				.Argument<NonNullGraphType<StringGraphType>>("title", "Título do livro")
				.ResolveAsync(async context => await bookRepository.GetByTitleAsync(context.GetArgument<string>("title"))
			);

			Field<ListGraphType<BookType>>("booksByAuthor")
				.Description("Busca os livros pelo autor")
				.Argument<NonNullGraphType<StringGraphType>>("author", "Autor do livro")
				.ResolveAsync(async context => await bookRepository.GetByAuthorAsync(context.GetArgument<string>("author"))
			);
		}
	}
}