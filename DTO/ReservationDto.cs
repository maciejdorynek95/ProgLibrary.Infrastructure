using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.DTO
{
    public class ReservationDto
    {
        public Guid? User { get;  set; }
        public Guid? Book { get;  set; }
        public DateTime ReservationTimeFrom { get; set; }
        public DateTime ReservationTimeTo { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool Reserved { get; protected set; }
    }
}
