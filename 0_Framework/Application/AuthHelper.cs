﻿using System;
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
                new Claim("Username", authViewModel.Username),
                new Claim("Type", authViewModel.Type.ToString()),

                // Or Use ClaimTypes.NameIdentifier
            };
            if(authViewModel.Permissions.Count>0)
                claims.Add(new Claim("Permissions", string.Join(',', authViewModel.Permissions)));

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

        public List<int> GetPermissionsUser()
        {
            if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                return new List<int>();
            return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Permissions")?
                .Value.Split(',',StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

        }

        public AuthViewModel GetUserInfo()
        {
            var claimUser = _contextAccessor.HttpContext.User.Claims.ToList();
            if (claimUser.Count == 0)
                return null;

            var accountInfo = new AuthViewModel()
            {
                Permissions = claimUser.FirstOrDefault(x => x.Type == "Permissions")?.Value.Split(',').Select(int.Parse)
                    .ToList(),
                RoleId = int.Parse(claimUser.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value),
                AccountId = int.Parse(claimUser.FirstOrDefault(x => x.Type == "AccountId")?.Value),
                Username = claimUser.FirstOrDefault(x => x.Type == "Username")?.Value,
                FullName = claimUser.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                Type = int.Parse(claimUser.FirstOrDefault(x => x.Type == "Type")?.Value)
            };
            return accountInfo;
        }

        public long GetUserId()
        {
            return int.Parse(_contextAccessor.HttpContext.User.Claims.First(x => x.Type == "AccountId").Value);
        }
    }
}