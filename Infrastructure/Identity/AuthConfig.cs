using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chat.Models
{
    public static class AuthConfig
    {
        public static string ISSUER = "ServerApp";
        public static string AUDIENCE = "ClientApp";
        public static string KEY = "minimumSixteenCharacters";
        public const int LIFETIME = 2;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
