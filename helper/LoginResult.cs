using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOUR.helper
{
    public class LoginResult
    {

        public string UserName { get; set; }
        public string RefreshToken { get; set; }

        public object Role { get; set; }
        public String AccessToken { get; set; }



    }
}
