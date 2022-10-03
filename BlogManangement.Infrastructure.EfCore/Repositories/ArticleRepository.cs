using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EfCore.Repositories
{
    public class ArticleRepository:RepositoryBase<long,Article>,IArticleRepository
    {
        private readonly BlogContext _blogContext;
        public ArticleRepository(BlogContext context) : base(context)
        {
            _blogContext = context;
        }

        public Article GetWithCategory(long id)
        {
            return _blogContext.Articles.Include(x => x.ArticleCategory).FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel model)
        {
            var query = _blogContext.Articles.AsNoTracking();

            if (model.CategoryId != 0)
                query = query.Where(x => x.CategoryId == model.CategoryId);

            if (!string.IsNullOrWhiteSpace(model.Title))
                query = query.Where(x => x.Title.Contains(model.Title));


            return query.Include(x => x.ArticleCategory).OrderByDescending(x => x.Id).Select(x => new ArticleViewModel()
            {
                CategoryName = x.ArticleCategory.Name,
                Title = x.Title,
                CreationDate = x.CreationDate.ToFarsi(),
                Id = x.Id,
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                ShortDescription = x.ShortDescription
            }).ToList();
        }
    }
}
