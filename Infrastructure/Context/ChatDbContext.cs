using System;
using System.Collections.Generic;
using System.Text;
using Chat.Application.Common.Data;
using Chat.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Context
{
    public class ChatDbContext : DbContext, IChatDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Offer> Offers { get; set; }

        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Relation>()
            .HasOne(u => u.Sender)
            .WithMany(u => u.SendedRelation)
            .HasForeignKey(rl => rl.SenderId);

            modelBuilder.Entity<Relation>()
            .HasOne(u => u.Receiver)
            .WithMany(U => U.ReceivedRelations)
            .HasForeignKey(rl => rl.ReceiverId);

            modelBuilder.Entity<Message>()
             .HasOne(u => u.from)
             .WithMany(u => u.SendedMessages)
             .HasForeignKey(rl => rl.fromId);

            modelBuilder.Entity<Message>()
            .HasOne(u => u.to)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(rl => rl.toId);

            modelBuilder.Entity<Offer>()
            .HasOne(u => u.Sender)
            .WithMany(u => u.SendedOffers)
            .HasForeignKey(rl => rl.SenderId);

            modelBuilder.Entity<Offer>()
            .HasOne(u => u.Receiver)
            .WithMany(u => u.ReceivedOffers)
            .HasForeignKey(rl => rl.ReceiverId);

        }
    }
}
