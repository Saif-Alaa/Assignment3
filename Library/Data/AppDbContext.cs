using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Dtos
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Member> members { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<Borrowing> borrowings { get; set; }
        public DbSet<Category> categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .Property(c => c.Description)
                .HasMaxLength(200);

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();

            modelBuilder.Entity<Book>()
                .Property(b => b.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Book>()
                .Property(b => b.IsAvailable)
                .HasDefaultValue(true);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Books)
                .WithOne(b => b.Category)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.Email)
                .IsUnique();

            modelBuilder.Entity<Member>()
                .HasMany(m => m.Borrowings)
                .WithOne(b => b.Member)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Book>()
                .HasMany(b => b.Borrowings)
                .WithOne(br => br.Book)
                .HasForeignKey(br => br.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Programming",
                    Description = "Programming and software development books"
                },
                new Category
                {
                    Id = 2,
                    Name = "Database",
                    Description = "Database and data management books"
                }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "C++ Basics",
                    Author = "John Smith",
                    ISBN = "ISBN001",
                    Price = 450,
                    IsAvailable = true,
                    CategoryId = 1
                },
                new Book
                {
                    Id = 2,
                    Title = "SQL Fundamentals",
                    Author = "Sarah Ali",
                    ISBN = "ISBN002",
                    Price = 500,
                    IsAvailable = false,
                    CategoryId = 2
                }
            );

            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    Id = 1,
                    FullName = "Ahmed Hassan",
                    Email = "ahmed@example.com",
                    Phone = "01012345678"
                },
                new Member
                {
                    Id = 2,
                    FullName = "Sara Mohamed",
                    Email = "sara@example.com",
                    Phone = "01198765432"
                }
            );

            modelBuilder.Entity<Borrowing>().HasData(
                new Borrowing
                {
                    Id = 1,
                    MemberId = 1,
                    BookId = 1,
                    BorrowDate = new DateTime(2026, 9, 20),
                    ReturnDate = new DateTime(2026, 9, 25)
                },
                new Borrowing
                {
                    Id = 2,
                    MemberId = 2,
                    BookId = 2,
                    BorrowDate = new DateTime(2026, 9, 22),
                    ReturnDate = null
                }
            );
        }
    }
}