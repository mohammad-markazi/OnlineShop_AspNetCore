using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.Article;
using _01_LampShadeQuery.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampShadeQuery.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategoriesInArticleDetail()
        {
            return _blogContext.ArticleCategories.Select(x => new ArticleCategoryQueryModel()
            {
                Name = x.Name,
                ArticlesCount = x.Articles.Count,
                Slug = x.Slug
            }).ToList();
        }

        public ArticleCategoryQueryModel GetArticleCategoryBySlug(string slug)
        {
            var articleCategory= _blogContext.ArticleCategories.Include(x=>x.Articles).Select(x => new ArticleCategoryQueryModel()
            {
                Slug = x.Slug,
                Name = x.Name,
                Description = x.Description,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress,
                Articles = MapArticles(x.Articles)
            }).FirstOrDefault(x => x.Slug == slug);

            articleCategory!.KeywordList = articleCategory.Keywords.Split(",").ToList();

            return articleCategory;
        }

        public List<ArticleCategoryQueryModel> GetAllArticleCategories()
        {
            return _blogContext.ArticleCategories.Select(x => new ArticleCategoryQueryModel()
            {
                Slug = x.Slug,
                Name = x.Name
            }).ToList();
        }

        private static List<ArticleQueryModel> MapArticles(List<Article> articles)
        {
            return articles.Select(x => new ArticleQueryModel()
            {
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Title = x.Title,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                PublishDate = x.PublishDate.ToFarsi(),

            }).ToList();
        }
    }
}
