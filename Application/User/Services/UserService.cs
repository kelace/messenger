using Chat.Application.Common.Data;
using Chat.Application.Factories;
using Chat.Application.Interfaces;
using Chat.Application.Services;
using Chat.Application.ViewModel;
using Chat.Domain.Entities;
using Chat.Domain.Entities.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private IChatDbContext _db;
        private IAccountModelFactory _accountModelFactory;
        private IInterlocutorModelFactory _interlocutorModelFactory;
        private IWorkContext _workContext;
        private IUserRepository _userRepository;

        public UserService(IChatDbContext db, IAccountModelFactory accountModelFactory, IInterlocutorModelFactory interlocutorModelFactory, IWorkContext workContext, IUserRepository userRepository)
        {
            _db = db;
            _accountModelFactory = accountModelFactory;
            _interlocutorModelFactory = interlocutorModelFactory;
            _workContext = workContext;
            _userRepository = userRepository;
        }
        public async Task<User> GetUserByNameAsync(string UserName)
        {
            return await _userRepository.Get(user => user.Name == UserName);
        }
        public async Task<List<InterlocutorVm>> SearchUserAsync(string letter, string Id)
        {
            List<InterlocutorVm> usersModel = new List<InterlocutorVm>();

            var users = await _userRepository.GetAll(u => u.Name.Contains(letter) && u.Id != Id);

            foreach (var user in users)
            {
                var userModel = await _interlocutorModelFactory.Form(user);
                usersModel.Add(userModel);
            }

            return usersModel;
        }
        public async Task<User> Get(Expression<Func<User, bool>> predicate)
        {
            return await _db.Users.Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<User> GetById(string Id)
        {
            return await _db.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<User> GetByName(string Name)
        {
            return await _db.Users.Where(u => u.Name == Name).FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserExist(string name)
        {
            var user = await _db.Users.Where(u => u.Name == name).FirstOrDefaultAsync();

            if (user != null) return true;
            return false;
        }

        public async Task<bool> CreateUser(string Name)
        {
            var user = new User(Name);

            await _userRepository.Create(user);

            var result = await _db.SaveChangesAsync();

            if (result == 0) return false;

            return true;
        }
    }
}
