using System.Collections.Generic;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Shared;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class IndexModel :BasePageModel
    {
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;

        public IndexModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }
        public List<ArticleViewModel> Articles { get; set; }
        public SelectList ArticleCategories { get; set; }
        [BindProperty(SupportsGet = true)]
        public  ArticleSearchModel SearchModel { get; set; }
        [NeedPermission(BlogPermissions.ListArticle)]

        public void OnGet()
        
        {
          Articles= _articleApplication.Search(SearchModel);
          ArticleCategories = new SelectList(_articleCategoryApplication.GetAll(), "Id", "Name");
        }

    }
}
