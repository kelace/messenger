using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services
{
    public interface IAuthenticationChatService
    {
        public Task<string> Authenticate(string Name, string Password);
        public void SignOut();
        public Task<bool> CreateIdentity(string Name, string Password, string UserId);
    }
}
