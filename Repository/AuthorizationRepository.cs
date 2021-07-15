using AuthorizationMicroservice.Helper;
using AuthorizationMicroservice.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Repository
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private IConfiguration iconfig;
        public AuthorizationRepository(IConfiguration _config)
        {
            iconfig = _config;
        }

        public User AuthenticateUser(int userId, string password)
        {
            foreach (var user in UserData.userList)
            {
                if (user.userId == userId && user.password == password)
                {
                    return user;
                }
            }
            return null;
        }

        public string GenerateJsonWebToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iconfig["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(iconfig["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public User UserDetails(int userId)
        {
            foreach (var user in UserData.userList)
            {
                if (user.userId == userId)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
