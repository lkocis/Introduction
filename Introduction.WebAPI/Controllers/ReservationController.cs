using Microsoft.AspNetCore.Mvc;
using Introduction.Service.Common;
using Introduction.Service;

namespace Introduction.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        public IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("send-confirmation/{reservationId}")]
        public async Task<IActionResult> SendConfirmationAsync(Guid reservationId)
        {
            try
            {
                await _reservationService.SendBookingConfirmationEmailAsync(reservationId);
                return Ok("Booking confirmation email sent successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error sending email: {ex.Message}");
            }
        }
    }
}
