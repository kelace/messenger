using Chat.Application.Account;
using Chat.Application.Account.Settings;
using Chat.Application.ViewModel;
using System.Threading.Tasks;

namespace Chat.Application.Services
{
    public interface IAccountService
    {
        public Task<AccountVm> GetUserDetails(string Id);
        public Task<AccountVm> GetUserDetails(string Id, string interlocutorId);
        public Task<bool> CreateAccount(string Name, string Password);
        public Task<bool> ChangeSettings(IPhotoFormFile photoForm);
    }
}