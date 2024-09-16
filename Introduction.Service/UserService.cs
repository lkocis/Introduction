using Introduction.Model;
using Introduction.Repository.Common;
using Introduction.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service
{
    public class UserService : IUserService
    {
        protected IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        public UserService(IUserRepository userRepository, IEmailService emailService)
        {
            _userRepository = userRepository;
            _emailService = emailService;
        }
        public async Task<User> GetUserInfoAsync(Guid id)
        {
            User user = await _userRepository.GetUserInfoAsync(id);
            return user;
        }

        public async Task<bool> SendMessage(User recepient, string messageText, Reservation reservation)
        {
            var emailMessage = $"Hello {recepient.FirstName} {recepient.LastName}, \nYour reservation {reservation.Id} from {reservation.DateFrom} to {reservation.DateTo} for {reservation.ReservationType.Type} has been made.";
            await _emailService.SendEmailAsync(recepient.Email, "Reservation Info", emailMessage);
            return true;
        }


    }
}
