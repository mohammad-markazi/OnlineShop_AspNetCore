using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Infrastructure.EfCore;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Domain.CommentAgg.ApplicationContacts;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace CommentManagement.Infrastructure.EfCore
{
    public class CommentRepository:RepositoryBase<long,Comment>,ICommentRepository
    {
        private readonly CommentContext _commentContext;
        private readonly ShopContext _shopContext;
        private readonly BlogContext _blogContext;
        public CommentRepository(CommentContext context, ShopContext shopContext, BlogContext blogContext) : base(context)
        {
            _commentContext = context;
            _shopContext = shopContext;
            _blogContext = blogContext;
        }

        public List<CommentViewModel> Search(CommentSearchModel model)
        {
            var query = _commentContext.Comments.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(model.Name))
                query = query.Where(x => x.Name.Contains(model.Name));
            if (!string.IsNullOrWhiteSpace(model.Email))
                query = query.Where(x => x.Email.Contains(model.Email));

            return  query.Select(x => new CommentViewModel()
            {
                CreationDate = x.CreationDate.ToFarsi(),
                Email = x.Email,
                Id = x.Id,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                Message = x.Message,
                Name = x.Name,
                EntityId = x.EntityId,
                EntityName = MapEntity(x,_blogContext,_shopContext)

            }).ToList();
            
        }

        private static string MapEntity(Comment comment,BlogContext blogContext,ShopContext shopContext)
        {
            if (comment.EntityType == EntityType.Article)
                return blogContext.Articles.Find(comment.EntityId).Title;
            return shopContext.Products.Find(comment.EntityId).Name;

        }
    }
}
