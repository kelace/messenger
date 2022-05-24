using Chat.Application;
using Chat.Application.Common.Data;
using Chat.Application.Interfaces;
using Chat.Application.Services;
using Chat.Domain.Entities;
using Chat.Domain.Entities.Repository;
using Chat.Domain.Entities.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class RelationService : IRelationService
    {
        IUserService _UserManager;
        IChatDbContext _db;
        IUserDomainService _userDomainService;
        IUserRepository _userRepository;
        IWorkContext _workContext;
        public RelationService(IUserService UserManager, IChatDbContext db, IUserDomainService userDomainService, IUserRepository userRepository, IWorkContext workContext)
        {
            _UserManager = UserManager;
            _db = db;
            _userDomainService = userDomainService;
            _userRepository = userRepository;
            _workContext = workContext;
        }

        public async Task<bool> CreateOfferAsync(string senderId, string receiverId)
        {

            var sender = await _userRepository.Get(u => u.Id == senderId);

            var receiver = await _userRepository.Get(u => u.Id == receiverId);

            if (!_userDomainService.IsOfferExist(sender, receiver)) throw new Exception("Offer already exist");

            var offer = sender.SendOfferToUser(receiver);

            receiver.ReceiveOffer(offer);

            _db.Users.Update(sender);

            _db.Users.Update(receiver);

            var result = await _db.SaveChangesAsync();

            if(result == 0) throw new Exception("Cant create new offer");

            return true;
        }

        public async Task CreateRelationAsync(string to, string from)
        {
            var friendUser =  await _userRepository.Get(f => f.Id == to);

            var currentUser =  await _userRepository.Get(f => f.Id == from);

            if (_userDomainService.IsRelationBetweenUserExist(currentUser, friendUser)) new Exception("Relations already exists");

            _userDomainService.AcceptOffer(friendUser, currentUser);

            var offer = _userDomainService.FindOfferBetweenUsers(currentUser, friendUser);
            
            _db.Users.Update(currentUser);

            _db.Users.Update(friendUser);

            _db.Offers.Remove(offer);

            var result = await _db.SaveChangesAsync();
        }

        public async Task<int> RemoveRelationAsync(DeclineRelationOptions options)
        {
            var currentUser = await _db.Users.Where(user => user.Id == options.CurrentUserId).SingleOrDefaultAsync();

            var user = await _db.Users.Where(user => user.Id == options.Id).SingleOrDefaultAsync();

            currentUser.RemoveUserRelation(user);

            user.RemoveUserRelation(currentUser);

            _db.Users.Update(currentUser);

            _db.Users.Update(user);

            await _db.SaveChangesAsync();

            return 1;
        }

        public async Task DeclineOfferAsync(string id)
        {
            var currentUser = await _userRepository.Get(user => user.Id == _workContext.GetUserId());

            var user = await _userRepository.Get(user => user.Id == id);

            var offer = _userDomainService.FindOfferBetweenUsers(currentUser, user);

            _db.Users.Update(currentUser);

            _db.Users.Update(user);

            _db.Offers.Remove(offer);

            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsRelateTo(string firstId, string secondId)
        {
            var relation = await _db.Relations.Where(r => (r.ReceiverId == firstId && r.SenderId == secondId) || (r.SenderId == firstId && r.ReceiverId == secondId)).SingleOrDefaultAsync();

            if (relation != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveOfferAsync(string currentUserId, string userId)
        {
            var currentUser = await _userRepository.Get(user => user.Id == currentUserId);

            var user = await _userRepository.Get(user => user.Id == userId);

            var offer = _userDomainService.FindOfferBetweenUsers(currentUser, user);

            _db.Users.Update(currentUser);

            _db.Offers.Remove(offer);

            await _db.SaveChangesAsync();

            return true;
        }

    }
}
