using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel :BasePageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }
      
        public List<SelectListItem> Categories { get; set; }
        public List<ProductViewModel> Products { get; set; }

        [BindProperty(SupportsGet = true)]
        public  SearchProductModel SearchModel { get; set; }

        [NeedPermission(ShopPermissions.ListProduct)]
        public void OnGet()

        {
            Categories =_productCategoryApplication.GetAll().Select(x => new SelectListItem(x.Name,x.Id.ToString())).ToList();

            Products = _productApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct()
            {
                Categories = _productCategoryApplication.GetAll()
            };
            return Partial("./Create", command);
        }
        [NeedPermission(ShopPermissions.CreateProduct)]
        public JsonResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var product = _productApplication.GetDetail(id);
            product.Categories = _productCategoryApplication.GetAll();

            return Partial("./Edit", product);
        }
        [NeedPermission(ShopPermissions.EditProduct)]

        public JsonResult OnPostEdit(EditProduct command)
        {
            var result = _productApplication.Edit(command);

            return new JsonResult(result);
        }


        
    }
}
