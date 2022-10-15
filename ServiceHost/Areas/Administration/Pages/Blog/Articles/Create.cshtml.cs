using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    [NeedPermission(BlogPermissions.CreateArticle)]

    public class CreateModel : PageModel
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;

        public CreateModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }
        public SelectList ArticleCategories { get; set; }
        [BindProperty]
        public CreateArticle Article { get; set; }
        public void OnGet()
        {
            ArticleCategories = new SelectList(_articleCategoryApplication.GetAll(), "Id", "Name");
            Article = new CreateArticle();
        }

        public IActionResult OnPost()
        {
            var result = _articleApplication.Create(Article);

            return RedirectToPage("./Index");
        }
    }
}
