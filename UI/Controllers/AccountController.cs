using Chat.Application.Account;
using Chat.Application.Common.Data;
using Chat.Application.Interfaces;
using Chat.Application.Services;
using Chat.Infrastructure.Photo;
using Chat.Models.Request;
using Chat.UI.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ChatControllerBase
    {
        IUserService _userService;
        IWebHostEnvironment _webHostEnvironment;
        IAccountService _accountService;
        IAuthenticationChatService _authenticationService;
        IChatDbContext _db;
        IWorkContext _workContext;
        public AccountController(IUserService userService, IWebHostEnvironment webHostEnvironment, IChatDbContext db, IAccountService accountService, IAuthenticationChatService authenticationService, IWorkContext workContext)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
            _db = db;
            _accountService = accountService;
            _authenticationService = authenticationService;
            _workContext = workContext;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("model");
            }

            var user = await _userService.GetByName(request.Name);


            if (user == null)
            {
                return BadRequest("no such user registred");
            }

            string token = await _authenticationService.Authenticate(request.Name, request.Password);

            _workContext.SetUser(user);
            var accountVm = await _accountService.GetUserDetails(user.Id);

            accountVm.Token = token;

            return Ok(accountVm);
        }

        [HttpPost]
        [Route("StartPoint")]
        public async Task<IActionResult> StartPoint(string dialogId)
        {
            var IsAuthenticated = User.Identity.IsAuthenticated;

            if (!IsAuthenticated)
            {
                return Unauthorized();
            }

            var Id = GetUserId();

            if (dialogId != null)
            {
                var resultDialog = await _accountService.GetUserDetails(Id, dialogId);
                return Ok(resultDialog);
            }

            var result = await _accountService.GetUserDetails(Id);

            return Ok(result);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {

            var isExist = await _userService.IsUserExist(request.Name);

            if (isExist) return Conflict(new { message = $"User with the name '{request.Name}' has already exist" });

            var result = await _accountService.CreateAccount(request.Name, request.Password);

            if (!result)
            {
                return BadRequest("Something gone wrong");
            }

            var token = await _authenticationService.Authenticate(request.Name, request.Password);

            var user  = await  _userService.GetByName(request.Name);

            var accountVm = await _accountService.GetUserDetails(user.Id);

            accountVm.Token = token;

            return Ok(accountVm);
        }

        [HttpPost]
        [Route("GetAccountInitialData")]
        [Authorize]
        public async Task<IActionResult> GetAccountInitialData()
        {
            var Id = User.Claims.First(f => f.Type == "userId").Value;
            var user = await _userService.GetById(Id);

            if (Id == null)
            {
                new Exception("user is not authenticated");
            }

            var result = await _accountService.GetUserDetails(Id);

            return Ok(result);
        }

        [HttpPost]
        [Route("GetAccountInitialDataWithDialogUser")]
        [Authorize]
        public async Task<IActionResult> GetAccountInitialDataWithDialogUser(string dialogId)
        {

            var Id = User.Claims.First(f => f.Type == "userId").Value;
            var user = await _userService.GetById(Id);

            if (Id == null)
            {
                new Exception("user is not authenticated");
            }

            var result = await _accountService.GetUserDetails(Id, dialogId);

            return Ok(result);
        }

        [HttpPost]
        [Route("ChangeAccountSettings")]
        [Authorize]
        public async Task<IActionResult> ChangeAccountSettings([FromForm] ChangeAccountSettings settings)
        {

            var photo = new PhotoFormFile(settings.photo);

            await _accountService.ChangeSettings(photo);

            return Ok();
        }


        [HttpPost]
        [Route("LogOut")]

        public IActionResult LogOut()
        {
            _authenticationService.SignOut();
            return Ok();
        }

    }
}
