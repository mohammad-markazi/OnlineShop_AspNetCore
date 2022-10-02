using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
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
    }
}
