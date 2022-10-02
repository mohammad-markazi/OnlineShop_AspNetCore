using System.Collections.Generic;
using _0_Framework.Application;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class ArticleCategorySearchModel
    {
        public string Name { get; set; }
    }

    public interface IArticleCategoryApplication
    {
        OperationResult Create(CreateArticleCategory command);
        OperationResult Edit(EditArticleCategory command);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel model);

        EditArticleCategory GetDetail(long id);

        List<ArticleCategoryViewModel> GetAll();
    }
}