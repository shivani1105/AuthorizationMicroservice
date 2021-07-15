using AuthorizationMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroservice.Helper
{
    public class UserData
    {
        public static List<User> userList = new List<User>()
        {
            new User{ userId=1, userName="Saiteja", password="saiteja"},
            new User{ userId=2, userName = "Shivani", password="shivani"},
            new User{ userId=3, userName="Chaitanya", password="chaitanya"},
            new User{ userId=4, userName="Thilak", password="thilak"},
            new User{ userId=5, userName="Ranjan", password="ranjan"}
        };
    }
}
