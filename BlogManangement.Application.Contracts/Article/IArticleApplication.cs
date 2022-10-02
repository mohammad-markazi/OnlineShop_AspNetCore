using System.Collections.Generic;
using _0_Framework.Application;

namespace BlogManagement.Application.Contracts.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticle command);

        OperationResult Edit(EditArticle command);

        List<ArticleViewModel> Search(ArticleSearchModel model);

        EditArticle GetDetail(long id);
    }
}