using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repositories
{
    public class RoleRepository:RepositoryBase<long,Role>,IRoleRepository
    {
        public RoleRepository(AccountContext context) : base(context)
        {
        }
    }
}
