using GraphQL;
using GraphQL.Types;
using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Interfaces.Repositories;
using LibraryApp.Infrastructure.GraphQL.Types;

namespace LibraryApp.Infrastructure.GraphQL.Mutations
{
	public class BookMutations : ObjectGraphType
	{
		public BookMutations(IBookRepository bookRepository)
		{
			Field<BookType>("createBook")
				.Description("Criar um novo livro")
				.Argument<NonNullGraphType<StringGraphType>>("title", "Título do livro")
				.Argument<NonNullGraphType<StringGraphType>>("author", "Autor do livro")
				.Argument<NonNullGraphType<StringGraphType>>("isbn", "ISBN do livro")
				.Argument<NonNullGraphType<IntGraphType>>("publicationYear", "Ano de publicação")
				.Argument<NonNullGraphType<StringGraphType>>("publisher", "Editora do livro")
				.Argument<NonNullGraphType<IntGraphType>>("totalCopies", "Total de exemplares")
				.Argument<NonNullGraphType<StringGraphType>>("genre", "Gênero do livro")
				.Argument<StringGraphType>("description", "Descrição do livro")
				.ResolveAsync(async context =>
				{
					var book = new Book(
						context.GetArgument<string>("title"),
						context.GetArgument<string>("author"),
						context.GetArgument<string>("isbn"),
						context.GetArgument<int>("publicationYear"),
						context.GetArgument<string>("publisher"),
						context.GetArgument<int>("totalCopies"),
						context.GetArgument<string>("genre"),
						context.GetArgument<string>("description")
					);

					await bookRepository.AddAsync(book);
					await bookRepository.SaveChangesAsync();
					return book;
				});

			Field<BookType>("updateBook")
				.Description("Atualizar um livro existente")
				.Argument<NonNullGraphType<IdGraphType>>("id", "ID do livro")
				.Argument<StringGraphType>("title", "Título do livro")
				.Argument<StringGraphType>("author", "Autor do livro")
				.Argument<StringGraphType>("isbn", "ISBN do livro")
				.Argument<IntGraphType>("publicationYear", "Ano de publicação")
				.Argument<StringGraphType>("publisher", "Editora do livro")
				.Argument<IntGraphType>("totalCopies", "Total de exemplares")
				.Argument<StringGraphType>("genre", "Gênero do livro")
				.Argument<StringGraphType>("description", "Descrição do livro")
				.ResolveAsync(async context =>
				{
					var book = await bookRepository.GetByIdAsync(context.GetArgument<Guid>("id"));

					if (book == null)
						throw new ExecutionError("Livro não encontrado");

					book.UpdateBookInfo(
						context.GetArgument<string>("title"),
						context.GetArgument<string>("author"),
						context.GetArgument<string>("isbn"),
						context.GetArgument<int>("publicationYear"),
						context.GetArgument<string>("publisher"),
						context.GetArgument<int>("totalCopies"),
						context.GetArgument<string>("genre"),
						context.GetArgument<string>("description")
					);

					await bookRepository.UpdateAsync(book);
					await bookRepository.SaveChangesAsync();
					return book;
				});

			Field<BookType>("deleteBook")
				.Description("Deletar um livro")
				.Argument<NonNullGraphType<IdGraphType>>("id", "ID do livro")
				.ResolveAsync(async context =>
				{
					var book = await bookRepository.GetByIdAsync(context.GetArgument<Guid>("id"));

					if (book == null)
						return false;

					await bookRepository.DeleteAsync(book);
					await bookRepository.SaveChangesAsync();
					return true;
				});
		}
	}
}