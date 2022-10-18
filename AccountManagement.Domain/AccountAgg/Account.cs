using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account:EntityBase
    {
        public string FullName { get;private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }
        public string Mobile { get; private set; }
        public string Profile { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }
        public long Type { get; private set; }
        public Account(string fullName, string username, string password, string mobile, string profile, long roleId, long type)
        {
            FullName = fullName;
            Username = username;
            Password = password;
            Mobile = mobile;
            Profile = profile;
            RoleId = roleId;
            Type = type;
        }

        public void Edit(string fullName, string username, string mobile, string profile, long roleId,long type)
        {
            FullName = fullName;
            Username = username;
            Mobile = mobile;
            if(!string.IsNullOrWhiteSpace(profile))
                Profile = profile;
            RoleId = roleId;
            Type = type;
        }

        public void ChangePassword(string password) => Password = password;

    }
}
