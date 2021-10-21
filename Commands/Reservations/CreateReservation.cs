using ProgLibrary.Infrastructure.Services;
using System;

namespace ProgLibrary.Infrastructure.Commands.Reservations
{
    public class CreateReservation
    {


        //public Guid Id { get;   set; }
        public Guid UserId { get;   set; }
        public Guid BookId { get;   set; }
        public DateTime ReservationTimeFrom { get;   set; }
        public DateTime ReservationTimeTo { get;  set; } 
        //public  DateTime CreatedAt { get;   set; }




    }



}
