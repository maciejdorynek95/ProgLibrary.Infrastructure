using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.ViewModels
{
    public class ReservationViewModel
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public DateTime ReservationTimeFrom { get; set; }
        [Required]
        public DateTime ReservationTimeTo { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }

    }
}
