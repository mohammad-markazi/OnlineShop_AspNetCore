using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repositories
{
    public class AccountRepository:RepositoryBase<long,Account>,IAccountRepository
    {
        private readonly AccountContext  _context;
        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public List<AccountViewModel> Search(AccountSearchModel model)
        {
            var query = _context.Accounts.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(model.FullName))
                query = query.Where(x => x.FullName.Contains(model.FullName));


            if (!string.IsNullOrWhiteSpace(model.Username))
                query = query.Where(x => x.FullName.Contains(model.Username));

            if (!string.IsNullOrWhiteSpace(model.Mobile))
                query = query.Where(x => x.FullName.Contains(model.Mobile));

            if(model.RoleId!=0)
                query = query.Where(x => x.RoleId==model.RoleId);

            return query.Include(x=>x.Role).Select(x=> new AccountViewModel()
            {
                FullName = x.FullName,
                Id = x.Id,
                Username = x.Username,
                Mobile = x.Mobile,
                Profile = x.Profile,
                Role = x.Role.Name,
                CreationDate = x.CreationDate.ToFarsi()
            }).OrderByDescending(x=>x.Id).ToList();

        }

        public Account GetByUsername(string username)
        {
            return _context.Accounts.SingleOrDefault(x => x.Username == username);
        }

        public EditAccount GetDetail(long id)
        {
            var account= _context.Accounts.FirstOrDefault(x => x.Id == id);
            if (account == null)
                return null;
            return new EditAccount()
            {
                Id = account.Id,
                FullName = account.FullName,
                Mobile = account.Mobile,
                Password = account.Password,
                RoleId = account.RoleId,
                Username = account.Username
            };
        }
    }
}
