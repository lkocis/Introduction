using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service.Common
{
    public interface IReservationService
    {
        Task<Reservation> GetReservationById(Guid id);
        Task<bool> SendBookingConfirmationEmailAsync(Guid reservationId);
    }
}
