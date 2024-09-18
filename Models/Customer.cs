using Labb4_MVC_Razer.Models;
using System.ComponentModel.DataAnnotations;

namespace Labb4_MVC_Razor.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Förnamn måste anges")]
        [StringLength(30, ErrorMessage = "Namnet får inte vara längre än 30 tecken")]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn måste anges")]
        [StringLength(30, ErrorMessage = "Namnet får inte vara längre än 30 tecken")]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-post")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }

        public ICollection<BookLoan>? BookLoans { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
