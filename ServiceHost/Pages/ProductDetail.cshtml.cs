using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceHost.Areas.Administration.Pages.Shared;
using ShopManagement.Application.Contracts.Comment;

namespace ServiceHost.Pages
{
    public class ProductDetailModel : BasePageModel
    {
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;

        public ProductDetailModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public ProductQueryModel Product { get; set; }
        public void OnGet(string id)
        {
            Product = _productQuery.GetDetailBy(id);
        }

        public IActionResult OnPost(AddComment comment)
        {
          var result=  _commentApplication.Add(comment);
          if(result.IsSuccess)
              Alert(result.Message,NotificationType.Success);
          else
              Alert(result.Message,NotificationType.Error);

          return RedirectToPage("./ProductDetail",new {Id=comment.ProductSlug});
        }
    }
}
