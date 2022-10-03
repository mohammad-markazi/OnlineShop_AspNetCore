using System.Collections.Generic;
using System.Globalization;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication:IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        {
            var operation=new OperationResult();

            if (_articleRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var category = _articleCategoryRepository.Get(command.CategoryId);

            var slug = command.Slug.Slugify();
            var fileName = _fileUploader.Upload(command.Picture, "Blogs", category.Slug, command.Slug);

            var article = new Article(command.Title,
                command.ShortDescription, command.Description, fileName,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug, command.CanonicalAddress, command.CategoryId, command.PublishDate.ToGeorgianDateTime());
            _articleRepository.Create(article);
            _articleRepository.SaveChanges();

            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operation = new OperationResult();
            if (_articleRepository.Exists(x =>x.Id!=command.Id&& x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var article = _articleRepository.GetWithCategory(command.Id);
            if (article == null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);
            var slug = command.Slug.Slugify();
            if(command.Picture!=null)
                _fileUploader.RemoveFile(article.Picture);

            var filename = _fileUploader.Upload(command.Picture, article.ArticleCategory.Slug, slug);

            article.Edit(command.Title,
                command.ShortDescription, command.Description, filename,
                command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug, command.CanonicalAddress, command.CategoryId, command.PublishDate.ToGeorgianDateTime());

            _articleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ArticleViewModel> Search(ArticleSearchModel model)
        {
            return _articleRepository.Search(model);
        }

        public EditArticle GetDetail(long id)
        {
            var article = _articleRepository.Get(id);

            return new EditArticle()
            {
                Id = article.Id,
                Title = article.Title,
                PictureAlt = article.PictureAlt,
                Slug = article.Slug,
                PictureTitle = article.PictureTitle,
                Description = article.Description,
                CategoryId = article.CategoryId,
                Keywords = article.Keywords,
                MetaDescription = article.MetaDescription,
                CanonicalAddress = article.CanonicalAddress,
                PublishDate = article.PublishDate.ToString(CultureInfo.InvariantCulture),
                ShortDescription = article.ShortDescription
            };
        }
    }
}
