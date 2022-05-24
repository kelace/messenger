using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Interfaces
{
    public interface IMessageService
    {
        public Task<Message> CreateMessageAsync(CreateMessageOptions options);
        public Task<List<Message>> GetAllDialogMessages(GetAllDialogMessagesOptions options);
    }
}
