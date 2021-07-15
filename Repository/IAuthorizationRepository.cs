using AuthorizationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Repository
{
    public interface IAuthorizationRepository
    {
        public User AuthenticateUser(int userId, string password);
        public string GenerateJsonWebToken(User user);
        public User UserDetails(int userId);
    }
}
