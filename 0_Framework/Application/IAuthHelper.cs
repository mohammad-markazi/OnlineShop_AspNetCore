using System.Collections.Generic;

namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SignIn(AuthViewModel authViewModel);

        void SignOut();

        string CurrentAccountRole();

        List<int> GetPermissionsUser();

        AuthViewModel GetUserInfo();

        long GetUserId();

    }
}
