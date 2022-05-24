using Chat.Application.Account;
using Chat.Application.Factories;
using Chat.Application.Interfaces;
using Chat.Application.Services;
using Chat.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Chat.Infrastructure.Photo;
using Chat.Infrastructure.Repositories;
using Chat.Domain.Entities.Repository;

namespace Chat.Infrastructure.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection ConfigureChatServices(this IServiceCollection service)
        {
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IRelationService, RelationService>();
            service.AddTransient<IMessageService, MessageService>();
            service.AddTransient<IAccountModelFactory, AccountModelFactory>();
            service.AddTransient<IInterlocutorModelFactory, InterlocutorModelFactory>();
            service.AddScoped<IWorkContext, WorkContext>();
            service.AddTransient<IAccountService, AccountService>();
            service.AddTransient<IAuthenticationChatService, AuthenticationService>();
            service.AddTransient<IPhotoFactory, PhotoFactory>();
            service.AddTransient<IUserRepository, UserRepository>();

            return service;
        }
    }
}
