using System;

namespace ProgLibrary.Infrastructure.DTO
{
    public class BookDto
    {

        public Guid Id { get; set; } 
        public string Title { get;  set; }
        public string Author { get;  set; }
        public DateTime ReleaseDate { get;  set; }
        public string Description { get; set; }
        public int BookReservations { get; set; }

    }
}
