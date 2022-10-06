using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account
{
    public class CreateAccount
    {
        public string FullName { get;  set; }

        public string Username { get;  set; }

        public string Password { get;  set; }
        public string Mobile { get;  set; }
        public IFormFile Profile { get;  set; }
        public long RoleId { get;  set; }
    }
}
