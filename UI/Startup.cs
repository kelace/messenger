using chat.Models;
using Chat.Application.Common.Data;
using Chat.Application.Common.DependencyInjection;
using Chat.Domain.Entities;
using Chat.Infrastructure.Context;
using Chat.Infrastructure.Extensions;
using Chat.Infrastructure.Identity;
using Chat.Infrastructure.RealTime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chat.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ChatDbContext>(options =>
                          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Chat.Infrastructure")));
           
            services.AddDbContext<ChatIdentityDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("Chat.Infrastructure")));

            services.AddScoped<IChatDbContext>(provider => provider.GetService<ChatDbContext>());

            services.AddSpaStaticFiles(configuration: options =>
            {

                options.RootPath = "VueChat/dist";

            });

            services.AddControllers(option => option.EnableEndpointRouting = false)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                }
            );

            services.AddHttpContextAccessor();

            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            services.AddIdentity<ChatIdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })

            .AddEntityFrameworkStores<ChatIdentityDbContext>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthConfig.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/realTime")))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.ConfigureChatServices();
            services.AddSignalR()
                .AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.Converters
                       .Add(new JsonStringEnumConverter());
                });

            services.AddApplicationServices();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(
                  options =>
                  {
                    options.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "application/json";
                            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                            if (null != exceptionObject)
                            {
                                var errorMessage = $"<b>Exception Error: {exceptionObject.Error.Message} </b> {exceptionObject.Error.StackTrace}";
                                await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                            }
                        });
                  }
              );

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/realTime");
                endpoints.MapControllers();

            });

            app.UseSpa(spaBuilder =>
            {
                spaBuilder.Options.SourcePath = "VueChat";
            });
        }
    }
}
