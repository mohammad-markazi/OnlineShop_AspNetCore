using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Domain.RoleAgg
{
    public class Permission
    {
        public long Id { get;private set; }

        public int  Code{ get; private set; }

        public string Name { get; private set; }
        public long RoleId { get; private set; }

        public Role Role { get; private set; }
        public Permission(int code)
        {
            Code = code;

        }
        public Permission(int code, string name)
        {
            Code = code;
            Name = name;
        }
    }
    public class Role:EntityBase
    {
        public string Name { get;private set; }
        public List<Account> Accounts { get; private set; }
        public int Type { get; private set; }
        public List<Permission> Permissions { get;private set; }
        public Role(string name)
        {
            Name = name;
        }
        public Role(string name, int type)
        {
            Type = type;
            Name = name;

        }
        public Role(string name,int type, List<Permission> permissions)
        {
            Type = type;
            Name = name;
            Permissions=permissions;
        }

        public void Edit(string name,int type,List<Permission> permissions)
        {
            Name=name;
            Type=type;
            Permissions=permissions;
        }

    }

 

  
}
