using Chat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Common.Data
{
    public interface IChatDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
