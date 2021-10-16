using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.DTO
{
    public class BookDetailsDto : BookDto
    {
        public IEnumerable<ReservationDto> Reservations { get; set; }
    }
}
