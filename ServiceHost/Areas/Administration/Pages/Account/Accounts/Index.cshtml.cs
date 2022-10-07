using System.Collections.Generic;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;

namespace ServiceHost.Areas.Administration.Pages.Account.Accounts
{
    public class IndexModel :BasePageModel
    {
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;

        public IndexModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }


        public List<AccountViewModel> Accounts { get; set; }
        public SelectList Roles { get; set; }
        [BindProperty(SupportsGet = true)]
        public  AccountSearchModel SearchModel { get; set; }
        public void OnGet()

        {
            Roles = new SelectList(_roleApplication.GetAll(), "Id", "Name");
            Accounts = _accountApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateAccount()
            {
                Roles = _roleApplication.GetAll(),
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate(CreateAccount command)
        {
            var result = _accountApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var account = _accountApplication.GetDetail(id);
            account.Roles = _roleApplication.GetAll();
            return Partial("./Edit", account);
        }

        public JsonResult OnPostEdit(EditAccount command)
        {
            var result = _accountApplication.Edit(command);

            return new JsonResult(result);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            var command = new ChangePassword()
            {
                Id = id
            };
            return Partial("./ChangePassword", command);
        }
        public JsonResult OnPostChangePassword(ChangePassword command)
        {
            var result = _accountApplication.ChangePassword(command);

            return new JsonResult(result);
        }

    }
}
