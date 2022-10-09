using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace _0_Framework.Application
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        

        public void SignIn(AuthViewModel authViewModel)
        {
            var claims = new List<Claim>
            {
                new Claim("AccountId", authViewModel.AccountId.ToString()),
                new Claim(ClaimTypes.Name, authViewModel.FullName),
                new Claim(ClaimTypes.Role, authViewModel.RoleId.ToString()),
                new Claim("Username", authViewModel.Username), // Or Use ClaimTypes.NameIdentifier
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                IsPersistent = authViewModel.Remember
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public string CurrentAccountRole()
        {
            if (_contextAccessor.HttpContext.User.Identity != null && _contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x=>x.Type==ClaimTypes.Role)!.Value;
            return null;
        }
    }
}