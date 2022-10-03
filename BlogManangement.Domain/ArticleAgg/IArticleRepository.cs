using System.Collections.Generic;
using _0_Framework.Domain;
using BlogManagement.Application.Contracts.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository:IRepository<long,Article>
    {
        Article GetWithCategory(long id);

        List<ArticleViewModel> Search(ArticleSearchModel model);
    }
}