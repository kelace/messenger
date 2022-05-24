using Chat.Application.Account;
using Chat.Infrastructure.Photo;
using Microsoft.AspNetCore.Http;

namespace Chat.UI.Models.Request
{
    public class ChangeAccountSettings
    {
        public IFormFile photo { get; set; }
    }
}
