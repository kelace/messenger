using Chat.Domain.Entities;
using Chat.Domain.Entities.Repository;
using Chat.Domain.Persistence.Repository;
using Chat.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        ChatDbContext _db;

        public UserRepository(ChatDbContext db)
        {
            _db = db;
        }
        public async Task Create(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task<User> Get(Expression<Func<User, bool>> predicate)
        {
            return await _db.Users
                .Include(u => u.SendedRelation)
                .Include(u => u.ReceivedRelations)
                .Include(u => u.SendedOffers)
                .Include(u => u.ReceivedOffers)
                .Include(u => u.SendedMessages)
                .Include(u => u.ReceivedMessages)
                .Where(predicate).SingleOrDefaultAsync();
        }

        public async Task<List<User>> GetAll()
        {
            return await _db.Users
                .Include(u => u.SendedRelation)
                .Include(u => u.ReceivedRelations)
                .Include(u => u.SendedOffers)
                .Include(u => u.ReceivedOffers)
                .Include(u => u.SendedMessages)
                .Include(u => u.ReceivedMessages)
               .ToListAsync();
        }

        public async Task<List<User>> GetAll(Expression<Func<User, bool>> predicate)
        {
            return await _db.Users
                .Include(u => u.SendedRelation)
                .Include(u => u.ReceivedRelations)
                .Include(u => u.SendedOffers)
                .Include(u => u.ReceivedOffers)
                .Include(u => u.SendedMessages)
                .Include(u => u.ReceivedMessages)
           .Where(predicate)
           .ToListAsync();
        }

        public void Remove(User user)
        {
            _db.Users.Remove(user);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public void Update(User user)
        {
            _db.Users.Update(user);
        }
    }
}
