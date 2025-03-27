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
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(user => user.Id);

			builder.Property(user => user.Name)
				.HasMaxLength(200)
				.IsRequired();

			builder.Property(user => user.Email)
				.HasMaxLength(200)
				.IsRequired();

			builder.Property(User => User.DocumentId)
				.HasMaxLength(20)
				.IsRequired();

			builder.Property(user => user.Phone)
				.HasMaxLength(20)
				.IsRequired();

			builder.Property(user => user.Address)
				.HasMaxLength(200)
				.IsRequired();

			builder.HasIndex(user => user.Email)
				.IsUnique();

			builder.HasIndex(user => user.DocumentId)
				.IsUnique();
		}
	}
}