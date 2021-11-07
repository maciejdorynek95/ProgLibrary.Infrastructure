using ProgLibrary.Infrastructure.DTO;
using System.Collections.Generic;

namespace ProgLibrary.Infrastructure.ViewModels
{
    public class AccountDetailsViewModel : AccountViewModel
    {
        public string[] Roles { get; set; }
        public IEnumerable<ReservationDto> Reservations { get; set; }
    }
}
