using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb4_MVC_Razer.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Display(Name = "Boktitel")]
        [MaxLength(65)]
        public string BookName { get; set; } = string.Empty;
        [Display(Name = "Författare")]
        [MaxLength(65)]
        public string Author { get; set; } = string.Empty;
        [Display(Name = "Beskrivning")]
        [MaxLength(400)]
        public string? BookDescription { get; set; }
        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;
        [MaxLength(25)]
        public string ISBN { get; set; } = string.Empty;
        [Display(Name = "Tillgänglig att låna")]
        public bool InStock { get; set; } = false;
        [Display(Name = "Utgivningsår")]
        public DateTime ReleaseYear { get; set; }

        [Display(Name = "Bild URL")]
        [DataType(DataType.ImageUrl)]
        public string? ImageUrl { get; set; }

        public ICollection<BookLoan>? BookLoans { get; set; }
    }
}
