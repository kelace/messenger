using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.UI.Controllers
{
    public class ChatControllerBase : Controller
    {
        protected string GetUserId()
        {
            return User.Claims.First(f => f.Type == "userId").Value;
        }
    }
}
