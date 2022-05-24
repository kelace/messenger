using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Application.Authentication.Errors
{
    class AuthenticationException : Exception
    {
        public AuthenticationException() : base("Authentication exception, login or password incorrect")
        {

        }
    }
}
