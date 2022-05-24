using chat.Models;
using Chat.Application.Interfaces;
using Chat.Application.Services;
using Chat.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationChatService
    {
        SignInManager<ChatIdentityUser> _signInManager;
        IUserService _userService;
        UserManager<ChatIdentityUser> _userManager;
        IHttpContextAccessor _httpContextAccessor;
        public AuthenticationService(SignInManager<ChatIdentityUser> signInManager, IUserService userService, UserManager<ChatIdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userService = userService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> Authenticate(string name, string password)
        {
            var user = await _userManager.FindByNameAsync(name);
            var result = await _userManager.CheckPasswordAsync(user, password);

            if (!result) new AuthenticationException();

            var token = CreateJwt(user);


            return token;
        }

        private ClaimsPrincipal CreateClaimsPrincipal(ChatIdentityUser user)
        {
            var identity = new ClaimsIdentity(GetClaims(user), JwtBearerDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }

        public async Task<bool> CreateIdentity(string Name, string Password, string UserId)
        {
            var user = new ChatIdentityUser { UserName = Name, UserId = UserId };
            var identity = await _userManager.CreateAsync(user, Password);

            return identity.Succeeded;
        }

        public async void SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        private string CreateJwt(ChatIdentityUser user)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    notBefore: now,
                    claims: GetClaims(user),
                    expires: now.Add(TimeSpan.FromDays(AuthConfig.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthConfig.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        private Claim[] GetClaims(ChatIdentityUser user)
        {
            var claims = new[]
            {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "user"),
                    new Claim("userId", user.UserId),
            };

            return claims;
        }
    }
}
