using System.Collections.Generic;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account
{
    public interface IAccountApplication
    {
        OperationResult Register(RegisterAccount command);

        OperationResult Edit(EditAccount command);

        OperationResult ChangePassword(ChangePassword command);

        List<AccountViewModel> Search(AccountSearchModel model);

        EditAccount GetDetail(long id);

        OperationResult<AccountViewModel> Login(Login command);
    }
}