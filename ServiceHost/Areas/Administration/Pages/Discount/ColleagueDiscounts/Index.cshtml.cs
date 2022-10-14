using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Discount.ColleagueDiscounts
{
    public class IndexModel :BasePageModel
    {
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(IColleagueDiscountApplication colleagueDiscountApplication, IProductApplication productApplication)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
        }
        public List<ColleagueDiscountViewModel> ColleagueDiscounts { get; set; }
        public SelectList Products { get; set; }
        [BindProperty(SupportsGet = true)]
        public  ColleagueDiscountSearchModel SearchModel { get; set; }
        [NeedPermission(DiscountPermissions.ListColleagueDiscount)]

        public void OnGet()

        {
            Products = new SelectList(_productApplication.GetAll(), "Id", "Name");

            ColleagueDiscounts = _colleagueDiscountApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount();
           command.Products = _productApplication.GetAll().ToDictionary(x => x.Id, x => x.Name);
            return Partial("./Create", command);
        }
        [NeedPermission(DiscountPermissions.CreateColleagueDiscount)]

        public JsonResult OnPostCreate(DefineColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _colleagueDiscountApplication.GetDetail(id);
            product.Products = _productApplication.GetAll().ToDictionary(x => x.Id, x => x.Name);

            return Partial("./Edit", product);
        }
        [NeedPermission(DiscountPermissions.EditColleagueDiscount)]

        public JsonResult OnPostEdit(EditColleagueDiscount command)
        {
            var result = _colleagueDiscountApplication.Edit(command);

            return new JsonResult(result);
        }
        [NeedPermission(DiscountPermissions.RemoveColleagueDiscount)]

        public IActionResult OnGetRemoved(long id)
        {
            _colleagueDiscountApplication.Remove(id);
            return RedirectToPage("./Index");
        }
        [NeedPermission(DiscountPermissions.RestoreColleagueDiscount)]

        public IActionResult OnGetRestore(long id)
        {
            _colleagueDiscountApplication.Restore(id);
            return RedirectToPage("./Index");
        }

    }
}
