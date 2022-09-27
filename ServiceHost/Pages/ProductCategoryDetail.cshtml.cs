using _01_LampShadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductCategoryDetailModel : PageModel
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public ProductCategoryDetailModel(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public ProductCategoryQueryModel ProductCategory { get; set; }
        public void OnGet(string id)
        {
            ProductCategory = _productCategoryQuery.ProductCategoriesWithProductsBy(id);
        }
    }
}
