using Chat.Application.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Infrastructure.RealTime
{
    public class CustomUserIdProvider : IUserIdProvider
    {

        public string GetUserId(HubConnectionContext connection)
        {

            if (connection.User.Claims != null)
            {
                var id = connection.User.Claims.First(f => f.Type == "userId").Value;

                return id;
            }

            throw new Exception("Have no claims");
        }
    }
}
