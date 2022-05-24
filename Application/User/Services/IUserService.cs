using Chat.Application.ViewModel;
using Chat.Domain.Entities;
using Chat.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserByNameAsync(string userName);
        public Task<List<InterlocutorVm>> SearchUserAsync(string letter, string Id);
        public Task<User> Get(Expression<Func<User, bool>> predicate);
        public Task<User> GetById(string Id);
        public Task<User> GetByName(string Name);
        public Task<bool> IsUserExist(string name);
        public Task<bool> CreateUser(string Name);

    }
}
