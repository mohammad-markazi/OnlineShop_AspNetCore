using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Comment;
using ShopManagement.Domain.CommentAgg;

namespace ShopManagement.Application
{
    public class CommentApplication:ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult Add(AddComment command)
        {
            var operation=new OperationResult();

            var comment = new Comment(command.Name, command.Email, command.Message, command.ProductId);

            _commentRepository.Create(comment);
            _commentRepository.SaveChanges();

            return operation.Succeeded();
        }

        public void Confirm(long id)
        {
            var comment=_commentRepository.Get(id);

            comment.Confirm();
            _commentRepository.SaveChanges();
        }

        public void Canceled(long id)
        {
            var comment = _commentRepository.Get(id);

            comment.Canceled();
            _commentRepository.SaveChanges();
        }

        public List<CommentViewModel> Search(CommentSearchModel model)
        {
            var query = _commentRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(model.Name))
                query = query.Where(x => x.Name.Contains(model.Name));

            if (!string.IsNullOrWhiteSpace(model.Email))
                query = query.Where(x => x.Email.Contains(model.Email));

            return query.Include(x=>x.Product).Select(x => new CommentViewModel()
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
