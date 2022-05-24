
using Chat.Application.Common.Data;
using Chat.Application.Services;
using Chat.Application.ViewModel;
using Chat.Domain.Entities;
using Chat.Domain.Entities.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Application.Factories
{
    public class InterlocutorModelFactory : IInterlocutorModelFactory
    {
        IWorkContext _workContext;
        IChatDbContext _db;
        IUserRepository _userRepository;
        public InterlocutorModelFactory(IWorkContext workContext, IChatDbContext db, IUserRepository userRepository)
        {
            _workContext = workContext;
            _db = db;
            _userRepository = userRepository;
        }
        public async Task<InterlocutorVm> Form(string Id)
        {
            var user = await _userRepository.Get(user => user.Id == Id);
            return await Form(user);
        }
        public async Task<InterlocutorVm> Form(User user)
        {
            var currentUserId = _workContext.GetUserId();
            var currentUser = await _userRepository.Get(u => u.Id == currentUserId);
            var interlocutor = new InterlocutorVm { Name = user.Name, Id = user.Id, Status = OfferStatus.Nothing, IsThisUserInitiatorOfRelation = false};

            interlocutor.Photo = user.Photo;

            var relations = user.Relations;
            var relation = relations.SingleOrDefault(r => r.SenderId == currentUserId || r.ReceiverId == currentUserId);
            var offers = user.Offers;
            var offer = offers.SingleOrDefault(o => o.SenderId == currentUserId || o.ReceiverId == currentUserId);

            var messages = user.Messages.Where(m => (m.fromId == currentUserId && m.toId == user.Id) || (m.fromId == user.Id && m.toId == currentUserId)).OrderBy(m => m.DateCreate).ToList();
            interlocutor.Messages = messages;

            if (offer != null)
            {
                var relationInitiator = offer.SenderId == user.Id ? true : false ;
                interlocutor.Status = offer.Status;
                interlocutor.IsThisUserInitiatorOfRelation = relationInitiator;
            }

            if (relation != null)
            {
                interlocutor.Status = OfferStatus.Accepted;
            }

            return interlocutor;
        }
    }
}
