using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomerDiscounts
{
    public class IndexModel :BasePageModel
    {
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;

        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }
        public List<CustomerDiscountViewModel> CustomerDiscounts { get; set; }
        public SelectList Products { get; set; }
        [BindProperty(SupportsGet = true)]
        public  CustomerDiscountSearchModel SearchModel { get; set; }
        [NeedPermission(DiscountPermissions.ListCustomerDiscount)]

        public void OnGet()

        {
            Products = new SelectList(_productApplication.GetAll(), "Id", "Name");

            CustomerDiscounts = _customerDiscountApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount();
           command.Products = _productApplication.GetAll().ToDictionary(x => x.Id, x => x.Name);
            return Partial("./Create", command);
        }
        [NeedPermission(DiscountPermissions.CreateCustomerDiscount)]

        public JsonResult OnPostCreate(DefineCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _customerDiscountApplication.GetDetail(id);
            product.Products = _productApplication.GetAll().ToDictionary(x => x.Id, x => x.Name);

            return Partial("./Edit", product);
        }
        [NeedPermission(DiscountPermissions.EditCustomerDiscount)]

        public JsonResult OnPostEdit(EditCustomerDiscount command)
        {
            var result = _customerDiscountApplication.Edit(command);

            return new JsonResult(result);
        }

    }
}
