using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampShadeQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        List<ArticleCategoryQueryModel> GetArticleCategoriesInArticleDetail();
        ArticleCategoryQueryModel GetArticleCategoryBySlug(string slug);

        List<ArticleCategoryQueryModel> GetAllArticleCategories();
    }
}
