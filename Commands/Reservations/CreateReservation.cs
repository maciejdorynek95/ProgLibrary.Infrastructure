using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Commands.Reservations
{
    public class CreateReservation
    {
        public Guid Id { get; set; }
        public Guid UserId { get;  set; }
        public Guid BookId { get;  set; }
        public DateTime ReservationTimeFrom { get; set; }
        public DateTime ReservationTimeTo { get; set; }
        public DateTime ReservationTime { get; set; }

        public CreateReservation()
        {
            Id = Guid.NewGuid();
        }
    }

  
  
}
