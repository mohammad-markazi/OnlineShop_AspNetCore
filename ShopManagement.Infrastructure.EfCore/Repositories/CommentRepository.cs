using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class CommentRepository:RepositoryBase<long,Comment>,ICommentRepository
    {
        private readonly ShopContext _shopContext;
        public CommentRepository(ShopContext context) : base(context)
        {
            _shopContext = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel model)
        {
            var query = _shopContext.Comments.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(model.Name))
                query = query.Where(x => x.Name.Contains(model.Name));

            if (!string.IsNullOrWhiteSpace(model.Email))
                query = query.Where(x => x.Email.Contains(model.Email));

            return query.Include(x => x.Product).Select(x => new CommentViewModel()
            {
                CreationDate = x.CreationDate.ToFarsi(),
                Name = x.Name,
                Email = x.Email,
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                Message = x.Message
            }).ToList();
        }
    }
}
