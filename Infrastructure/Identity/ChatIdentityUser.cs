using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Infrastructure.Identity
{
    public class ChatIdentityUser : IdentityUser
    {
        public string UserId { get; set; }
    }
}
