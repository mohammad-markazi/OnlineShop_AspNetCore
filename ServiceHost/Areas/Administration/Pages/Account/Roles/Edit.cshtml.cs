using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Account.Roles
{
    public class EditModel : PageModel
    {
        private readonly IRoleApplication _roleApplication;
        public List<SelectListItem> Permissions=new List<SelectListItem>();
        private IEnumerable<IPermissionExposer> _exposers;
        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
        {
            _roleApplication = roleApplication;
            _exposers = exposers;
        }
        [BindProperty]
        public EditRole Role { get; set; }
        public void OnGet(long id)
        {
            Role = _roleApplication.GetDetail(id);
            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();

                foreach (var (key,value) in exposedPermissions)
                {
                    var group = new SelectListGroup()
                    {
                        Name = key,
                    };
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group= group
                        };
                        if(Role.MapPermissions.Any(x=>x.Code==permission.Code))
                            item.Selected = true;

                        Permissions.Add(item);
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            _roleApplication.Edit(Role);
            return RedirectToPage("./Index");
        }
    }
}
