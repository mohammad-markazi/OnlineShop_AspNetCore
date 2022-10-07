using System.Collections.Generic;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;

namespace ServiceHost.Areas.Administration.Pages.Account.Roles
{
    public class IndexModel :BasePageModel
    {
        private readonly IRoleApplication _roleApplication;

        public IndexModel( IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }


        public List<RoleViewModel> Roles { get; set; }
      
        public void OnGet()

        {
            Roles = _roleApplication.GetAll();
        }

        public IActionResult OnGetCreate()
        {
           
            return Partial("./Create", new CreateRole());
        }

        public JsonResult OnPostCreate(CreateRole command)
        {
            var result = _roleApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var role = _roleApplication.GetDetail(id);
           
            return Partial("./Edit", role);
        }

        public JsonResult OnPostEdit(EditRole command)
        {
            var result = _roleApplication.Edit(command);

            return new JsonResult(result);
        }


    }
}
