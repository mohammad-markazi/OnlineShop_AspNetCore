using _0_Framework.Application;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceHost.Areas.Administration.Pages.Shared;

namespace ServiceHost.Pages
{
    public class AccountModel : BasePageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IAuthHelper _authHelper;
        private readonly IRoleApplication _roleApplication;

        public AccountModel(IAccountApplication accountApplication, IAuthHelper authHelper, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _authHelper = authHelper;
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
            var s = Notification;

        }

        public IActionResult OnPostLogin(Login login)
        {
            var result = _accountApplication.Login(login);
            if (result.IsSuccess)
            {
                _authHelper.SignIn(new AuthViewModel()
                {
                    Remember = login.RememberMe,
                    FullName = result.Data.FullName,
                    RoleId = result.Data.RoleId,
                    Username = result.Data.Username,
                    AccountId = result.Data.Id
                });
                return RedirectToPage("./Index");
            }
            Alert(result.Message,NotificationType.Error);
            return RedirectToPage("./Account");
        }

        public IActionResult OnGetLogout()
        {
            _authHelper.SignOut();
            return RedirectToPage("./Index");

        }

        public IActionResult OnPostRegister(RegisterAccount account)
        {
            account.RoleId=_roleApplication.GetRoleByType(RoleTypes.NormalUser).Id;

          var result=  _accountApplication.Register(account);
          if (result.IsSuccess)
              Alert(result.Message,NotificationType.Success);
          else
              Alert(result.Message, NotificationType.Error);
          

            return RedirectToPage("./Account");

        }
    }
}
