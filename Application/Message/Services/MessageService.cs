using Chat.Application;
using Chat.Application.Common.Data;
using Chat.Application.Interfaces;
using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        IUserService _UserService;
        IRelationService _relationService;
        IChatDbContext _db;
        public MessageService(IUserService UserService, IRelationService relationService, IChatDbContext db)
        {
            _UserService = UserService;
            _relationService = relationService;
            _db = db;
        }
        public async Task<Message> CreateMessageAsync(CreateMessageOptions options)
        {
            var userFrom = await _UserService.GetById(options.fromID);

            var userTo = await _UserService.GetById(options.toID);

            var IsRelate = await _relationService.IsRelateTo(userFrom.Id, userTo.Id);

            if (!IsRelate)
            {
                new Exception("you have no relationship with this user");
            }

            var message = new Message() { from = userFrom, to = userTo, text = options.text };

            await _db.Messages.AddAsync(message);

            var result = await _db.SaveChangesAsync();

            if (result !=0)
            {
                return message;
            }

            return null;
        }

        public async Task<List<Message>> GetAllDialogMessages(GetAllDialogMessagesOptions options)
        {
            return await _db.Messages.Where(m => (m.fromId == options.fromID && m.toId == options.toID) || (m.fromId == options.toID && m.toId == options.fromID)).ToListAsync();
        }
    }
}
