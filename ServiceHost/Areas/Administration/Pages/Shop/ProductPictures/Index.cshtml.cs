using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel :BasePageModel
    {
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;

        public IndexModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
        }
      
        public List<SelectListItem> Products { get; set; }
        public List<ProductPictureViewModel> ProductPictures { get; set; }

        [BindProperty(SupportsGet = true)]
        public  ProductPictureSearchModel SearchModel { get; set; }
        [NeedPermission(ShopPermissions.ListProductPicture)]

        public void OnGet()

        {
             Products =_productApplication.GetAll().Select(x => new SelectListItem(x.Name,x.Id.ToString())).ToList();

            ProductPictures = _productPictureApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture()
            {
                Products = _productApplication.GetAll()
            };
            return Partial("./Create", command);
        }
        [NeedPermission(ShopPermissions.CreateProductPicture)]

        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {

            var productPicture = _productPictureApplication.GetDetail(id);
            productPicture.Products = _productApplication.GetAll();

            return Partial("./Edit", productPicture);
        }
        [NeedPermission(ShopPermissions.EditProductPicture)]

        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _productPictureApplication.Edit(command);

            return new JsonResult(result);
        }
        [NeedPermission(ShopPermissions.RestoreProductPicture)]

        public IActionResult OnGetRestore(long id)
        {
            _productPictureApplication.Restore(id);
            return RedirectToPage("./Index");
        }
        [NeedPermission(ShopPermissions.RemoveProductPicture)]

        public IActionResult OnGetRemove(long id)
        {
            _productPictureApplication.Remove(id);
            return RedirectToPage("./Index");
        }
    }
}
