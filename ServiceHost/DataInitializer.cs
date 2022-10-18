using System.Linq;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EfCore;

namespace ServiceHost
{
    public class DataInitializer : IDataInitializer
    {
        private readonly AccountContext _accountContext;
        private readonly IAccountApplication _accountApplication;
        public DataInitializer(AccountContext accountContext, IAccountApplication accountApplication)
        {
            _accountContext = accountContext;
            _accountApplication = accountApplication;
        }

        public void InitializeData()
        {
            var roleAdmin = _accountContext.Roles.SingleOrDefault(x => x.Type == RoleTypes.Super);
            var roleNormalUser = _accountContext.Roles.SingleOrDefault(x => x.Type == RoleTypes.Normal);
            if (roleNormalUser is null)
            {
                roleNormalUser = new Role("کاربر معمولی", RoleTypes.Normal);
                _accountContext.Roles.Add(roleNormalUser);
                _accountContext.SaveChanges();
            }

            if (roleAdmin is null)
            {
                roleAdmin = new Role("مدیر سیستم", RoleTypes.Super);
                _accountContext.Roles.Add(roleAdmin);
                _accountContext.SaveChanges();
            }



            var initAccount = new RegisterAccount()
            {
                Username = "Admin",
                FullName = "مدیر سیستم",
                RoleId = roleAdmin.Id,
                Mobile = "09033163381",
                Password = "1274089530"
            };

            if (!_accountContext.Accounts.Any(x => x.Username == initAccount.Username))
            {
                _accountApplication.Register(initAccount);
                _accountContext.SaveChanges();
            }


        }
    }
}
