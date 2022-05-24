
using Chat.Application.Account;
using Chat.Application.Account.Settings;
using Chat.Application.Common.Data;
using Chat.Application.Factories;
using Chat.Application.Interfaces;
using Chat.Application.ViewModel;
using Chat.Domain.Entities.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Application.Services
{
    public class AccountService : IAccountService
    {

        IAccountModelFactory _accountModelFactory;
        IUserService _userService;
        IAuthenticationChatService _authenticationService;
        IChatDbContext _db;
        IWorkContext _workContext;
        IPhotoFactory _photoFactory;
        IUserRepository _userRepository;

        public AccountService(IAccountModelFactory accountModelFactory, IUserService userService, IAuthenticationChatService authenticationService, IChatDbContext db, IWorkContext workContext, IPhotoFactory photoFactory, IUserRepository userRepository)
        {
            _accountModelFactory = accountModelFactory;
            _userService = userService;
            _authenticationService = authenticationService;
            _db = db;
            _workContext = workContext;
            _photoFactory = photoFactory;
            _userRepository = userRepository;
        }
        public async Task<AccountVm> GetUserDetails(string Id)
        {
            var user = await _userRepository.Get(u => u.Id == Id);

            if (user is null) throw new Exception("No such user");

            return await _accountModelFactory.Form(user);
        }
        public async Task<AccountVm> GetUserDetails(string Id, string InterlocutorId)
        {
            var user = await _userRepository.Get(u => u.Id == Id);

            var interlocutor = await _userRepository.Get(u => u.Id == InterlocutorId);

            return await _accountModelFactory.Form(user, interlocutor);
        }

        public async Task<bool> CreateAccount(string Name, string Password)
        {

            await _userService.CreateUser(Name);

            var user = await _userService.GetByName(Name);

            await _authenticationService.CreateIdentity(Name, Password, user.Id);

            return true;
        }

        public async Task<bool> ChangeSettings(IPhotoFormFile photoFormFile)
        {
            var Id = _workContext.GetUserId();

            var user = await _userService.GetById(Id);

            var fileName = await _photoFactory.CreatePhoto(photoFormFile);

            user.Photo = fileName;

            _db.Users.Update(user);

            var result = await _db.SaveChangesAsync();

            if (result == 0) return false;
            return true;
        }

    }
}
