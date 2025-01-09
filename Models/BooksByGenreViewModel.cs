using Labb4_MVC_Razer.Models;

namespace Labb4_MVC_Razor.Models
{
    public class BooksByGenreViewModel
    {
        public string Genre { get; set; }
        public List<Book> Books { get; set; }
    }

}
