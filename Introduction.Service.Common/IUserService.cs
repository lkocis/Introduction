using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service.Common
{
    public interface IUserService
    {
        Task<User> GetUserInfoAsync(Guid id);
        Task<bool> SendMessage(User recepient, string messageText, Reservation reservation);
    }
}
