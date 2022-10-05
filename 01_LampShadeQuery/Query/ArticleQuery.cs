using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.Article;
using _01_LampShadeQuery.Contracts.Product;
using BlogManagement.Infrastructure.EfCore;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampShadeQuery.Query
{
    public class ArticleQuery:IArticleQuery
    {
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;
        public ArticleQuery(BlogContext blogContext, CommentContext commentContext)
        {
            _blogContext = blogContext;
            _commentContext = commentContext;
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

        public ArticleQueryModel GetArticleDetail(string slug)
        {
            var article= _blogContext.Articles.Where(x => x.PublishDate <= DateTime.Now).Select(x =>
                new ArticleQueryModel()
                {
                    Id = x.Id,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Description = x.Description,
                    Slug = x.Slug,
                    CategorySlug = x.ArticleCategory.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Picture = x.Picture,
                    PublishDate = x.PublishDate.ToFarsi(),
                    CategoryName = x.ArticleCategory.Name,
                    ShortDescription = x.ShortDescription,
                    Title = x.Title,
                    CanonicalAddress = x.CanonicalAddress
                }).FirstOrDefault(x=>x.Slug==slug);
            if(article==null)
                return new ArticleQueryModel();
            article.Comments = MapComments(article.Id);
            article.KeywordList = article.Keywords.Split(",").ToList();

            return article;

        }

        private List<CommentQueryModel> MapComments(long articleId)
        {
            return _commentContext.Comments.Include(x=>x.Parent)
                .Where(x => x.EntityType == EntityType.Article && x.EntityId == articleId && x.IsConfirmed && !x.IsCanceled).Select(x =>
                    new CommentQueryModel()
                    {
                        Id = x.Id,
                        Message = x.Message,
                        Name = x.Name,
                        CreationDate = x.CreationDate.ToFarsi(),
                        ParentId = (long)((x.ParentId!=null)?x.ParentId:0),
                        ParentName =x.Parent!=null? x.Parent.Name:null
                    }).OrderByDescending(x => x.Id).ToList(); ;
        }
    }
}
