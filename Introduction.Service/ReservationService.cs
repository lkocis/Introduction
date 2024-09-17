using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Introduction.Model;
using Introduction.Repository.Common;
using Introduction.Service.Common;
using System.Net.Mail;
using System.Linq.Expressions;

namespace Introduction.Service
{
    public class ReservationService : IReservationService
    {
        public IReservationRepository _reservationRepository;
        private readonly IEmailService _emailService;

        public ReservationService(IReservationRepository reservationRepository, IEmailService emailService)
        {
            _reservationRepository = reservationRepository;
            _emailService = emailService;
        }

        public async Task<Reservation> GetReservationById(Guid id)
        {
            var reservation = await _reservationRepository.GetReservationById(id);
            return reservation;
        }

        public async Task<bool> SendBookingConfirmationEmailAsync(Guid reservationId)
        {
            var reservation = await _reservationRepository.GetReservationById(reservationId);

            if (reservation != null)
            {
                string subject = $"Booking Confirmation";
                string body = $@"
                        Dear {reservation.User.FirstName},
                        Your reservation in {reservation.HotelReservationType.Hotel.Name} has been confirmed.
                        Reservation Details:
                        Check-in: {reservation.DateFrom:MM/dd/yyyy}
                        Check-out: {reservation.DateTo:MM/dd/yyyy}
                        Total Price: {reservation.Price}$
                        Reservation type: {reservation.ReservationType.Type}
                        Thank you for booking with us.";

                await _emailService.SendEmailAsync(reservation.User.Email, subject, body);
                return true;
            }
            return false;
        }
    }
}
