using Labb4_MVC_Razer.Models;

namespace Labb4_MVC_Razor.Models
{
    public class BooksViewModel
    { 
            public IEnumerable<Book> Books { get; set; }
            public IEnumerable<string> Genres { get; set; }

    }
}
