using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel :BasePageModel
    {
        private readonly ISlideApplication _slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }
        public List<SlideViewModel> Slides { get; set; }

        [NeedPermission(ShopPermissions.ListSlide)]
        public void OnGet()
        {
            Slides = _slideApplication.GetAll();
        }

        public IActionResult OnGetCreate()
        {
           
            return Partial("./Create", new CreateSlide());
        }
        [NeedPermission(ShopPermissions.CreateSlide)]

        public JsonResult OnPostCreate(CreateSlide command)
        {
            var result = _slideApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {

            var slide = _slideApplication.GetDetail(id);
        

            return Partial("./Edit", slide);
        }
        [NeedPermission(ShopPermissions.EditSlide)]

        public JsonResult OnPostEdit(EditSlide command)
        {
            var result = _slideApplication.Edit(command);

            return new JsonResult(result);
        }
        [NeedPermission(ShopPermissions.RestoreSlide)]

        public IActionResult OnGetRestore(long id)
        {
            _slideApplication.Restore(id);
            return RedirectToPage("./Index");
        }
        [NeedPermission(ShopPermissions.RemoveSlide)]

        public IActionResult OnGetRemove(long id)
        {
            _slideApplication.Remove(id);
            return RedirectToPage("./Index");
        }
    }
}
