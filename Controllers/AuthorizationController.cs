using AuthorizationMicroservice.Models;
using AuthorizationMicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(AuthorizationController));

        IAuthorizationRepository iauth;
        public AuthorizationController(IAuthorizationRepository _iauth)
        {
            iauth = _iauth;

        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] User user)
        {
            log.Info("Authorization Controller - Login - Authentication has been Initiated for User Id: " + user.userId);

            try
            {
                IActionResult response = Unauthorized();
                var userValue = iauth.AuthenticateUser(user.userId, user.password);
                if (userValue != null)
                {
                    var tokenString = iauth.GenerateJsonWebToken(user);
                    log.Info("Authorization Controller - Login - Token Generated for UserId: " + user.userId);
                    response = Ok(tokenString);
                    log.Info("Authorization Controller - Login - Authentication Successful for User Id: " + user.userId);
                }
                else
                {
                    log.Info("Authorization Controller - Login - Authentication Unsuccessful for User Id: " + user.userId);
                }
                return response;
            }

            catch (Exception e)
            {
                log.Info("Authorizatioin Controller - Login - Exception " + e.StackTrace+ " BadRequest ");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetUserDetails/{userId}")]
        public IActionResult GetUserDetails(int userId)
        {
            try
            {
                var result = iauth.UserDetails(userId);

                if (result != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }

            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
