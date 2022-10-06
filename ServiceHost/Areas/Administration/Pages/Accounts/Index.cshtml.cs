using System.Collections.Generic;
using System.Linq;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;

namespace ServiceHost.Areas.Administration.Pages.Accounts
{
    public class IndexModel :BasePageModel
    {
        private readonly IAccountApplication _accountApplication;

        public IndexModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }


        public List<AccountViewModel> Accounts { get; set; }
        public SelectList Roles { get; set; }
        [BindProperty(SupportsGet = true)]
        public  AccountSearchModel SearchModel { get; set; }
        public void OnGet()

        {

            Accounts = _accountApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
          
            return Partial("./Create", new CreateAccount());
        }

        public JsonResult OnPostCreate(CreateAccount command)
        {
            var result = _accountApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var account = _accountApplication.GetDetail(id);

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
