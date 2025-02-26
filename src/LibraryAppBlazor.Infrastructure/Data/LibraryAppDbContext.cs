using LibraryAppBlazor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAppBlazor.Infrastructure.Data
{
	public class LibraryAppDbContext : DbContext
	{
		//DbSet represents a collection of entities that can be queried from the database.
		public DbSet<Book> Books { get; set; }

		public DbSet<Loan> Loans { get; set; }

		public LibraryAppDbContext(DbContextOptions<LibraryAppDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Configure the Book entity
			modelBuilder.Entity<Book>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.Property(e => e.Title)
					.IsRequired()
					.HasMaxLength(200);

				entity.Property(e => e.Author)
					.IsRequired()
					.HasMaxLength(200);

				entity.Property(e => e.ISBN)
					.IsRequired()
					.HasMaxLength(13);

				entity.Property(e => e.PublicationYear)
					.IsRequired();

				entity.Property(e => e.CreatedAt)
					.IsRequired();
			});

			//Configure the Loan entity
			modelBuilder.Entity<Loan>(entity =>
			{
				entity.HasKey(e => e.Id);

				entity.Property(e => e.BorrowerName)
					.IsRequired()
					.HasMaxLength(200);

				entity.Property(e => e.BorrowerEmail)
					.IsRequired()
					.HasMaxLength(200);

				entity.Property(e => e.LoanDate)
					.IsRequired();

				entity.Property(e => e.DueDate)
					.IsRequired();

				entity.Property(e => e.ReturnDate);

				//Relationship with the Book entity
				entity.HasOne(e => e.Book)
					.WithMany() //Define navigation property on the Book entity
					.HasForeignKey(e => e.BookId)
					.OnDelete(DeleteBehavior.Cascade); //Cascade delete when a book is deleted
			});
		}
	}
}