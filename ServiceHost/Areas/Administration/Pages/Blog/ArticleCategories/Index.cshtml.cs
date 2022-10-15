using System.Collections.Generic;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Areas.Administration.Pages.Shared;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories
{
    public class IndexModel :BasePageModel
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
        }
        public List<ArticleCategoryViewModel> ArticleCategories { get; set; }

        [BindProperty(SupportsGet = true)]
        public  ArticleCategorySearchModel SearchModel { get; set; }
        [NeedPermission(BlogPermissions.ListArticleCategory)]

        public void OnGet()
        
        {
          ArticleCategories= _articleCategoryApplication.Search(SearchModel);
        }

        public IActionResult OnGetCreate()
        {

            return Partial("./Create",new CreateArticleCategory());
        }
        [NeedPermission(BlogPermissions.CreateArticleCategory)]

        public JsonResult OnPostCreate(CreateArticleCategory command)
        {
            var result = _articleCategoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit(long id)
        {
            var articleCategory = _articleCategoryApplication.GetDetail(id);

            return Partial("./Edit", articleCategory);
        }
        [NeedPermission(BlogPermissions.EditArticleCategory)]

        public JsonResult OnPostEdit(EditArticleCategory command)
        {
            var result = _articleCategoryApplication.Edit(command);

            return new JsonResult(result);
        }
    }
}
