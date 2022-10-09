namespace _0_Framework.Application
{
    public interface IAuthHelper
    {
        void SignIn(AuthViewModel authViewModel);

        void SignOut();

        string CurrentAccountRole();

    }
}
