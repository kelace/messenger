using System.Threading.Tasks;

namespace Chat.Application.Interfaces
{
    public interface IRelationService
    {
        public Task CreateRelationAsync(string to, string from);
        public Task<bool> CreateOfferAsync(string senderId, string receiverId);
        public Task<int> RemoveRelationAsync(DeclineRelationOptions options);
        public Task<bool> IsRelateTo(string firstId, string secondId);
        public Task<bool> RemoveOfferAsync(string currentUser, string userId);
        public Task DeclineOfferAsync(string id);
    }
}
