using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication:IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;
        private readonly IFileUploader _fileUploader;
        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository, IFileUploader fileUploader)
        {
            _articleCategoryRepository = articleCategoryRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateArticleCategory command)
        {
            var operation=new OperationResult();
            if (_articleCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            var slug = command.Slug.Slugify();
            var fileName = _fileUploader.Upload(command.Picture, "Blogs", slug);

            var articleCategory = new ArticleCategory(command.Name, fileName, command.PictureAlt,
                command.PictureTitle, command.Description, command.ShowOrder, slug, command.Keywords,
                command.MetaDescription);

            _articleCategoryRepository.Create(articleCategory);
            _articleCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();
            if (_articleCategoryRepository.Exists(x =>x.Id!=command.Id && x.Name == command.Name))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

            var articleCategory = _articleCategoryRepository.Get(command.Id);
            if(articleCategory == null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);

            var slug = command.Slug.Slugify();
            _fileUploader.RemoveFile(articleCategory.Picture);
            var fileName = _fileUploader.Upload(command.Picture, "Blogs", slug);

            articleCategory.Edit(command.Name, fileName, command.PictureAlt,
                command.PictureTitle, command.Description, command.ShowOrder, slug, command.Keywords,
                command.MetaDescription);

            _articleCategoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel model)
        {
            var query = _articleCategoryRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(model.Name))
                query = query.Where(x => x.Name.Contains(model.Name));

            return query.OrderByDescending(x => x.Id).Select(x => new ArticleCategoryViewModel()
            {
                Description = x.Description,
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                ShowOrder = x.ShowOrder,
                CreationDate = x.CreationDate.ToFarsi()
            }).ToList();
        }

        public EditArticleCategory GetDetail(long id)
        {
           var articleCategory= _articleCategoryRepository.Get(id);
           return new EditArticleCategory()
           {
               CanonicalAddress = articleCategory.CanonicalAddress,
               Description = articleCategory.Description,
               Id = articleCategory.Id,
               Keywords = articleCategory.Keywords,
               MetaDescription = articleCategory.MetaDescription,
               Name = articleCategory.Name,
               Slug = articleCategory.Slug,
               PictureAlt = articleCategory.PictureAlt,
               PictureTitle = articleCategory.PictureTitle,
               ShowOrder = articleCategory.ShowOrder
           };
        }
    }
}
