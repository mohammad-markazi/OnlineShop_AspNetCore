using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using _0_Framework.Infrastructure;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repositories
{
    public class RoleRepository:RepositoryBase<long,Role>,IRoleRepository
    {
        private readonly AccountContext _accountContext;
        public RoleRepository(AccountContext context) : base(context)
        {
            _accountContext = context;
        }

        public Role GetByType(int type)
        {
            return _accountContext.Roles.FirstOrDefault(x => x.Type == type);
        }
    }
}
