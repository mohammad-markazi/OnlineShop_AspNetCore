using System.Collections.Generic;
using _01_LampShadeQuery.Contracts.Article;
using _01_LampShadeQuery.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryDetailModel : PageModel
    {
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IArticleQuery _articleQuery;

        public ArticleCategoryDetailModel(IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
        }
        public ArticleCategoryQueryModel ArticleCategory { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
        public void OnGet(string id)
        {
            ArticleCategory = _articleCategoryQuery.GetArticleCategoryBySlug(id);
            ArticleCategories = _articleCategoryQuery.GetArticleCategoriesInArticleDetail();

            LatestArticles = _articleQuery.GetLatestArticles();

        }
    }
}
