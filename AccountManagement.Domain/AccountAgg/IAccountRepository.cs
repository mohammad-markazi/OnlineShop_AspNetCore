using System.Collections.Generic;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository:IRepository<long,Account>
    {
        List<AccountViewModel> Search(AccountSearchModel model);

        EditAccount GetDetail(long id);
    }
}