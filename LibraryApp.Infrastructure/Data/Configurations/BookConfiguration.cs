using LibraryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Infrastructure.Data.Configurations
{
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.HasKey(book => book.Id);

			builder.Property(book => book.Title)
				.HasMaxLength(200)
				.IsRequired();

			builder.Property(book => book.Author)
				.HasMaxLength(200)
				.IsRequired();

			builder.Property(book => book.ISBN)
				.HasMaxLength(20)
				.IsRequired();

			builder.Property(book => book.Publisher)
				.HasMaxLength(200)
				.IsRequired();

			builder.Property(book => book.Genre)
				.HasMaxLength(200)
				.IsRequired();

			builder.HasIndex(book => book.ISBN)
				.IsUnique();
		}
	}
}