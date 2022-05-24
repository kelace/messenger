using Chat.Application;
using Chat.Application.Interfaces;
using Chat.Infrastructure.RealTime;
using Chat.UI.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace Chat.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ChatControllerBase
    {
        IMessageService _messageService;
        IHubContext<ChatHub> _hubContext;
        public MessageController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }
        [HttpPost]
        [Route("CreateMessage")]
        public async Task<IActionResult> CreateMessage([FromBody] CreateMessageRequest request)
        {
            var fromId = User.Claims.First(f => f.Type == "userId").Value;

            var options = new CreateMessageOptions() { toID = request.toId, fromID = fromId, text = request.text };

            var message = await _messageService.CreateMessageAsync(options);

            await _hubContext.Clients.User(request.toId).SendAsync("recieveMessage", new { text = message.text, DateCreate = message.DateCreate, toId = message.toId, fromId = message.fromId });
           
            return Ok();
        }

        [HttpPost]
        [Route("GetAllDialogMessages")]
        public async Task<IActionResult> GetAllDialogMessages ([FromForm] string Id)
        {
            var fromId = User.Claims.First(f => f.Type == "userId").Value;

            var options = new GetAllDialogMessagesOptions() { fromID = fromId, toID = Id };

            var messages = await _messageService.GetAllDialogMessages(options);

            return Ok(messages);
        }
    }
}
