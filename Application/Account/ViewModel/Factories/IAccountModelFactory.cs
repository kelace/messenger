
using Chat.Application.ViewModel;
using Chat.Domain.Entities;
using System.Threading.Tasks;

namespace Chat.Application.Factories
{
    public interface IAccountModelFactory
    {
        public Task<AccountVm> Form(User user);
        public Task<AccountVm> Form(User user, User interlocutor);
    }
}