using System;
using System.Security.Claims;

namespace NOUR.helper
{
    public interface IJwtAuthManager
    {

        public JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now);
    }
}