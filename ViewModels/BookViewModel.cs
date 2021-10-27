using System;

namespace ProgLibrary.Infrastructure.ViewModels
{
    public class BookViewModel
    {

        public Guid Id { get; set; } 
        public string Title { get;  set; }
        public string Author { get;  set; }
        public DateTime ReleaseDate { get;  set; }
        public string Description { get; set; }
        public int BookReservations { get; set; }

    }
}
