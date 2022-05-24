using Chat.Application.Common.Data;
using Chat.Application.Interfaces;
using Chat.Application.ViewModel;
using Chat.Domain.Entities;
using Chat.Domain.Entities.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Factories
{
    public class AccountModelFactory : IAccountModelFactory
    {
        IInterlocutorModelFactory _interlocutorModelFactory;
        IChatDbContext _db;
        IUserRepository _userRepository;

        public AccountModelFactory( IInterlocutorModelFactory interlocutorModelFactory, IChatDbContext db, IUserRepository userRepository)
        {
            _interlocutorModelFactory = interlocutorModelFactory;
            _db = db;
            _userRepository = userRepository;
        }

        private async Task<AccountVm> _Form(User user)
        {
            var itnerlocutors = user.Relations;
            var itnerlocutorsOffer = user.Offers.FindAll(r => r.ReceiverId == user.Id && r.SenderId != user.Id);

            var userList = new List<InterlocutorVm>();
            var accountModel = new AccountVm();

            for (int i = 0; i < itnerlocutors.Count; i++)
            {
                var el = itnerlocutors[i];
                var userRelationId = el.SenderId == user.Id ? el.ReceiverId : el.SenderId;
                var userRelation = await _userRepository.Get(user => user.Id == userRelationId);
                var interlocutor = await _interlocutorModelFactory.Form(userRelation);

                userList.Add(interlocutor);
            }

            for (int i = 0; i < itnerlocutorsOffer.Count; i++)
            {
                var el = itnerlocutorsOffer[i];
                var userRelationId = el.SenderId == user.Id ? el.ReceiverId : el.SenderId;
                var userRelation = await _userRepository.Get(user => user.Id == userRelationId);
                var interlocutor = await  _interlocutorModelFactory.Form(userRelation);

                userList.Add(interlocutor);
            }

            accountModel.Users = userList;
            accountModel.Name = user.Name;
            accountModel.Id = user.Id;
            accountModel.Photo = "/images/" + user.Photo;

            return accountModel;
        }

        public async Task<AccountVm> Form (User currentUserId)
        {
            return await _Form(currentUserId);
        }

        public async Task<AccountVm> Form (User currentUserId, User userDialog)
        {
            var result = await _Form(currentUserId);

            var dialogUserDto = await _interlocutorModelFactory.Form(userDialog);

            result.DialogUser = dialogUserDto;

            return result;
        }
    }
}
