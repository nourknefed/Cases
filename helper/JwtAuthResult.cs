using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NOUR.helper
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }

    }
}
