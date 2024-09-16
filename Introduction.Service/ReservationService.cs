using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Introduction.Model;
using Introduction.Repository.Common;
using Introduction.Service.Common;
using System.Net.Mail;

namespace Introduction.Service
{
    public class ReservationService : IReservationService
    {
        public IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<bool> PostReservationInfo(Reservation reservation)
        {
            bool isSuccessful = await _reservationRepository.PostReservationInfo(reservation);
            return isSuccessful;
        }
    }
}
