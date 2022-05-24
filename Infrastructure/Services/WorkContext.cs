using Chat.Application.Common.Data;
using Chat.Application.Services;
using Chat.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class WorkContext : IWorkContext
    {
        IHttpContextAccessor _httpContextAccessor;
        IChatDbContext _db;
        User currentUser;
        public WorkContext(IHttpContextAccessor httpContextAccessor, IChatDbContext db)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
        }
        public async Task<User> GetUser()
        {
            if (currentUser != null) return currentUser;

            var Id = GetUserId();
            var user = await _db.Users.Where(u => u.Id == Id).SingleOrDefaultAsync();
            return user;
        }

        public void SetUser(User user)
        {
            currentUser = user;
        }

        public string GetUserId()
        {
            if (currentUser != null) return currentUser.Id;

            return _httpContextAccessor.HttpContext.User.Claims.First(f => f.Type == "userId").Value;
        }
    }
}
