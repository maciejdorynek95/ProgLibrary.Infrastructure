using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.DTO
{
    public class BookDto
    {

        public Guid Id { get; set; } // Niepotrzebne? -> potrzebne w DTO
        public string Title { get;  set; }
        public string Author { get;  set; }
        public DateTime ReleaseDate { get;  set; }
        public string Description { get; set; }
        public int BooksReservations { get; set; }
        //public IEnumerable<Reservation> Reservations { get;  set; }
    }
}
