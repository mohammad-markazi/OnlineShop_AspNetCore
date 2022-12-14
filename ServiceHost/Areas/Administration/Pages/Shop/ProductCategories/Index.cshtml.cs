using System.Collections.Generic;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceHost.Areas.Administration.Pages.Shared;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel :BasePageModel
    {
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }

        public List<ProductCategoryViewModel> ProductCategories { get; set; }

        [BindProperty(SupportsGet = true)]
        public  SearchProductCategory SearchModel { get; set; }
        [NeedPermission(ShopPermissions.ListProductCategory)]

        public void OnGet()
        
        {
          ProductCategories= _productCategoryApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {

            return Partial("./Create",new CreateProductCategory());
        }
        [NeedPermission(ShopPermissions.CreateProductCategory)]

        public JsonResult OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategoryApplication.GetDetail(id);

            return Partial("./Edit",productCategory);
        }
        [NeedPermission(ShopPermissions.EditProductCategory)]

        public JsonResult OnPostEdit(EditProductCategory command)
        {
            var result = _productCategoryApplication.Edit(command);

            return new JsonResult(result);
        }
    }
}
