using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Infrastructure.RealTime
{

    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
              await Clients.All.SendAsync("Notify", " вошел в чат");
        }
    }
}
