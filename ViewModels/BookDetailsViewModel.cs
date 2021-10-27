using ProgLibrary.Infrastructure.DTO;
using System.Collections.Generic;

namespace ProgLibrary.Infrastructure.ViewModels
{
    public class BookDetailsViewModel : BookViewModel
    {
        public IEnumerable<ReservationDto> Reservations { get; set; }
    }
}
