using System;
using System.ComponentModel.DataAnnotations;

namespace ProgLibrary.Infrastructure.Commands.Books
{
    public class CreateBook
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }

        
    }
}
