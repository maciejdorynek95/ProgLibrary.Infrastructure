using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.DTO
{
    public class ReservationDto
    {
        public Guid Id { get; protected set; }
        public Guid? UserId { get;  set; }
        public Guid? BookId { get;  set; }
        public DateTime ReservationTimeFrom { get; set; }
        public DateTime ReservationTimeTo { get; set; }
        public DateTime ReservationDate { get; set; }
        //public bool Reserved { get; protected set; }
    }
}
