using System.Collections.Generic;
using _01_LampShadeQuery.Contracts.Article;
using _01_LampShadeQuery.Contracts.ArticleCategory;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Domain.CommentAgg.ApplicationContacts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceHost.Areas.Administration.Pages.Shared;

namespace ServiceHost.Pages
{
    public class ArticleDetailModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;

        public ArticleDetailModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryQuery;
            _commentApplication = commentApplication;
        }

        public ArticleQueryModel Article { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public void OnGet(string id)
        {
            Article = _articleQuery.GetArticleDetail(id);
            LatestArticles = _articleQuery.GetLatestArticles();
            ArticleCategories = _articleCategoryQuery.GetArticleCategoriesInArticleDetail();
        }


        public IActionResult OnPost(AddComment comment)
        {
            comment.EntityType = EntityType.Article;
            var result = _commentApplication.Add(comment);

            return RedirectToPage("./ArticleDetail", new { Id = comment.EntitySlug });
        }
    }
}
