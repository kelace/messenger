using chat.UI.Models.Request;
using Chat.Application;
using Chat.Application.Interfaces;
using Chat.Application.Services;
using Chat.Application.ViewModel;
using Chat.Domain.Entities;
using Chat.Infrastructure.RealTime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class RelationController : ChatControllerBase
    {
        public IUserService _userService;
        public IRelationService _relationServices;
        IHubContext<ChatHub> _hubContext;
        IWorkContext _workContext;

        public RelationController(IUserService userService, IRelationService relationServices, IHubContext<ChatHub> hubContext, IWorkContext workContext)
        {
            _userService = userService;
            _relationServices = relationServices;
            _hubContext = hubContext;
            _workContext = workContext;

        }
        [HttpPost]
        [Route("CreateRelation")]
        public async Task<IActionResult> CreateRelation([FromBody] CreateRelationRequest request)
        {
            var from = User.Claims.First(f => f.Type == "userId").Value;

            var options = new CreateRelationOptions { to = request.toId, from = from };

            await _relationServices.CreateRelationAsync(request.toId, from);

            var user = await _userService.GetById(from);

            await _hubContext.Clients.User(request.toId).SendAsync("ReceiveOffer", new { Name = user.Name, Id = user.Id, status = "Sended", belonging = "Reciever" });
                
            return Ok(new { status = "Relation has sended" });
        }

        [HttpPost]
        [Route("CreateOffer")]
        public async Task<IActionResult> CreateOffer(string Id)
        {
            var user = await _workContext.GetUser();

            var result = await _relationServices.CreateOfferAsync(user.Id, Id);

            await _hubContext.Clients.User(Id).SendAsync("ReceiveOffer", new InterlocutorVm { Name = user.Name, Id = user.Id, Status = OfferStatus.Sended, IsThisUserInitiatorOfRelation = true, Messages = new List<Message>() });

            return Ok(result);
        }

        [HttpPost]
        [Route("Accept")]
        public async Task<IActionResult> Accept(string Id)
        {
            AcceptRelationOptions options = new AcceptRelationOptions() { Id = Id };

            string currentUserId = User.Claims.First(f => f.Type == "userId").Value;

            options.currentUserId = currentUserId;

            await _relationServices.CreateRelationAsync(Id, currentUserId);

            var user = await _userService.GetById(currentUserId);

            await _hubContext.Clients.User(Id).SendAsync("AcceptOffer", new InterlocutorVm { Name = user.Name, Id = user.Id, Status = OfferStatus.Accepted, IsThisUserInitiatorOfRelation = true});
            
            return Ok();
        }

        [HttpPost]
        [Route("Decline")]
        public async Task<IActionResult> Decline(string Id)
        {
            await _relationServices.DeclineOfferAsync(Id);
        
            return Ok();
        }

        [HttpPost]
        [Route("RemoveOffer")]
        public async Task<IActionResult> RemoveOffer(string Id)
        {
            string currentUserId = User.Claims.First(f => f.Type == "userId").Value;

            var result = await _relationServices.RemoveOfferAsync(currentUserId, Id);

            if (result)
            {
                return Ok();
            }

            return new ObjectResult("Something wents wrong")
            {
                StatusCode = 500
            };
        }
    }
}
