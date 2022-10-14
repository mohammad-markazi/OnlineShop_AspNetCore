using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Account.Roles
{
    public class CreateModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;

        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }
        [BindProperty]
        public CreateRole Role { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Role.Type = RoleTypes.Custom;
            var result = _roleApplication.Create(Role);
            return RedirectToPage("./Index");
        }
    }
}
