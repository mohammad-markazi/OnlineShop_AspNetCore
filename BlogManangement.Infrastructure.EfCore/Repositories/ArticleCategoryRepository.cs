using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using BlogManagement.Domain.ArticleCategoryAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EfCore.Repositories
{
    public class ArticleCategoryRepository:RepositoryBase<long,ArticleCategory>,IArticleCategoryRepository
    {
        public ArticleCategoryRepository(BlogContext context) : base(context)
        {
        }
    }
}
