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
    }
}
