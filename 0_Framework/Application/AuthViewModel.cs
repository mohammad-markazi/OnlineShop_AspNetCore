using System.Collections.Generic;

namespace _0_Framework.Application
{
    public class AuthViewModel
    {
        public long AccountId { get; set; }

        public long RoleId { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }
        public bool Remember { get; set; }

        public List<int> Permissions { get; set; }
    }
}