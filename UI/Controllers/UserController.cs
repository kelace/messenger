using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chat.Application.Common.Data;
using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Chat.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ChatControllerBase
    {

        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("SearchUser")]
        public async Task<IActionResult> SearchUser(string letter)
        {
            var Id = GetUserId();
            var users = await _userService.SearchUserAsync(letter, Id);

            return Ok(users);
        }
    }
}
