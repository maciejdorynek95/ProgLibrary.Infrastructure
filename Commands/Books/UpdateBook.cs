using System;

namespace ProgLibrary.Infrastructure.Commands.Books
{
    public class UpdateBook
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }

    }
}
