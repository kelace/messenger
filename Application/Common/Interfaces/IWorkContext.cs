using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Services
{
    public interface IWorkContext
    {
        public Task<User> GetUser();
        public string GetUserId();
        public void SetUser(User user);
    }
}
