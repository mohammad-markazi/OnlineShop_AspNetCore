using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductQuery _productQuery;

        public ProductDetailModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }

        public ProductQueryModel Product { get; set; }
        public void OnGet(string id)
        {
            Product = _productQuery.GetDetailBy(id);
        }
    }
}
