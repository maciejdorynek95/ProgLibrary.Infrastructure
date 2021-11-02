using System;
using System.ComponentModel.DataAnnotations;

namespace ProgLibrary.Infrastructure.ViewModels
{
    public class BookViewModel
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; } 
        [Required]
        public string Title { get;  set; }
        [Required]
        public string Author { get;  set; }
        [Required]
        public DateTime ReleaseDate { get;  set; }
        [Required]
        public string Description { get; set; }
        [ScaffoldColumn(false)]
        public int BookReservations { get; set; }

    }
}
