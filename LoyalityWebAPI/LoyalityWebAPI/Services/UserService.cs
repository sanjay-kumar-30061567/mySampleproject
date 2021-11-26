using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyalityWebAPI.Services
{
    public class UserService : IUserService
    {
        public bool ValidateCredentials(string username, string password)
        {
            return username.Equals("adaniLoyalty") && password.Equals("JZLqPSggZUy8rLR9dc2A5Q%3d%3d");
        }
    }
}
