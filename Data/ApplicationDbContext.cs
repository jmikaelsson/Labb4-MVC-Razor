using Labb4_MVC_Razer.Models;
using Labb4_MVC_Razor.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb4_MVC_Razer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookLoan> BookLoans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookLoan>()
                .HasKey(bl => new { bl.CustomerId, bl.BookId });

            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Customer)
                .WithMany(c => c.BookLoans)
                .HasForeignKey(bl => bl.CustomerId);

            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Book)
                .WithMany(b => b.BookLoans)
                .HasForeignKey(bl => bl.BookId);
        }
    }
}
