using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.Article;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampShadeQuery.Query
{
    public class ArticleQuery:IArticleQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }
        // PictureAlt = x.PictureAlt,
        // PictureTitle = x.PictureTitle,
        // Description = x.Description,
        // Slug = x.Slug,
        // CategorySlug = x.ArticleCategory.Slug,
        // Keywords = x.Keywords,
        // MetaDescription = x.MetaDescription,
        // Picture = x.Picture,
        // PublishDate = x.PublishDate.ToFarsi(),
        // CategoryName = x.ArticleCategory.Name,
        // ShortDescription = x.ShortDescription,
        // Title = x.Title,
        // CanonicalAddress = x.CanonicalAddress
        public List<ArticleQueryModel> GetLatestArticles()
        {
            return _blogContext.Articles.Where(x => x.PublishDate <= DateTime.Now).Take(8).OrderByDescending(x=>x.CreationDate).Select(x =>
                new ArticleQueryModel()
                {
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription,
                    Title = x.Title,
                }).ToList();
        }
    }
}
