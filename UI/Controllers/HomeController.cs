using Chat.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ChatControllerBase
    {
        private IUserService _UserManager;
        public HomeController(IUserService UserManager)
        {
            _UserManager = UserManager;
        }
    }
}
