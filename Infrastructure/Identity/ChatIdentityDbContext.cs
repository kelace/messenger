using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Infrastructure.Identity
{
    public class ChatIdentityDbContext : IdentityDbContext
    {
        public DbSet<ChatIdentityUser> Identities { get; set; }
        public ChatIdentityDbContext(DbContextOptions<ChatIdentityDbContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}
