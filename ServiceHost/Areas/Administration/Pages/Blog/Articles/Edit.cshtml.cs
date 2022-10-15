using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    [NeedPermission(BlogPermissions.EditArticle)]

    public class EditModel : BasePageModel
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;

        public EditModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }
        public SelectList ArticleCategories { get; set; }
        [BindProperty]
        public EditArticle Article { get; set; }
        public void OnGet(long id)
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetAll(), "Id", "Name");
            Article = _articleApplication.GetDetail(id);
        }

        public IActionResult OnPost()
        {
            var result = _articleApplication.Edit(Article);
            return RedirectToPage("./Index");
        }
    }
}
