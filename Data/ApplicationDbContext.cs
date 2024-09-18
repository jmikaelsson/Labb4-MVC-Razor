using Labb4_MVC_Razer.Models;
using Labb4_MVC_Razor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Labb4_MVC_Razor.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookLoan> BooksLoans { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
