using Chat.Application.Account;
using Chat.Domain.Entities.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Application.Common.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IUserDomainService, UserDomainService>();

            return services;
        }
    }
}
