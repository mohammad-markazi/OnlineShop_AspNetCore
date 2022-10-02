using _0_Framework.Domain;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository:IRepository<long,Article>
    {
        Article GetWithCategory(long id);
    }
}