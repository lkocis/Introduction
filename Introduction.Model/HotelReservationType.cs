using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Model
{
    public class HotelReservationType
    {
        public Guid Id { get; set; }
        public Guid ReservationTypeId { get; set; }
        public Guid HotelId { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; } 
        public Guid? CreatedByUserId { get; set; } 
        public Guid? UpdatedByUserId { get; set; } 
        public Hotel? Hotel { get; set; }
    }
}
