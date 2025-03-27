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
	public class LoanConfiguration : IEntityTypeConfiguration<Loan>
	{
		public void Configure(EntityTypeBuilder<Loan> builder)
		{
			builder.HasKey(loan => loan.Id);

			//Relationships configuration

			builder.HasOne(loan => loan.Book)
				.WithMany()
				.HasForeignKey(loan => loan.BookId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(loan => loan.User)
				.WithMany()
				.HasForeignKey(loan => loan.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Property(loan => loan.LoanDate)
				.IsRequired();

			builder.Property(loan => loan.DueDate)
				.IsRequired();

			//Index configuration for efiiciently search for overdue loans
			builder.HasIndex(loan => new { loan.BookId, loan.UserId });
			builder.HasIndex(loan => loan.DueDate);
		}
	}
}