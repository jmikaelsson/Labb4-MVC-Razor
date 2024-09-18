using Labb4_MVC_Razor.Models;

namespace Labb4_MVC_Razer.Models
{
    public class BookLoan
    {
        public int BookLoanId { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public bool IsReturned => ReturnDate.HasValue;
    }
}
