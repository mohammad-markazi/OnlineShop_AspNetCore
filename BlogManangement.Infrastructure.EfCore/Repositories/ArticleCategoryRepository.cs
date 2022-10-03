using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EfCore.Repositories
{
    public class ArticleCategoryRepository:RepositoryBase<long,ArticleCategory>,IArticleCategoryRepository
    {
        private readonly BlogContext _blogContext;
        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
            _blogContext = context;
        }


        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel model)
        {
            var query = _blogContext.ArticleCategories.Include(x=>x.Articles).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(model.Name))
                query = query.Where(x => x.Name.Contains(model.Name));

            return query.OrderByDescending(x => x.Id).Select(x => new ArticleCategoryViewModel()
            {
                Description = x.Description,
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                ShowOrder = x.ShowOrder,
                CreationDate = x.CreationDate.ToFarsi(),
                ArticleCount = x.Articles.Count
            }).ToList();
        }
    }
}
