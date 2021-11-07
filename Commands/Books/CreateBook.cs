using System;
using System.ComponentModel.DataAnnotations;

namespace ProgLibrary.Infrastructure.Commands.Books
{
    public class CreateBook
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }

        
    }
}
