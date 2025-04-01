using AutoMapper;
using LibraryApp.Application.DTOs.Book;
using LibraryApp.Application.DTOs.Loan;
using LibraryApp.Application.DTOs.User;
using LibraryApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//Book Mapping
			CreateMap<Book, BookDto>();
			CreateMap<CreateBookDto, Book>()
				.ConstructUsing(dto => new Book(
					dto.Title,
					dto.Author,
					dto.ISBN,
					dto.PublicationYear,
					dto.Publisher,
					dto.TotalCopies,
					dto.Genre,
					dto.Description));

			//User Mapping
			CreateMap<User, UserDto>();
			CreateMap<CreateUserDto, User>()
				.ConstructUsing(dto => new User(
					dto.Name,
					dto.Email,
					dto.DocumentId,
					dto.Phone,
					dto.Address));

			//Loan Mapping
			CreateMap<Loan, LoanDto>();
		}
	}
}