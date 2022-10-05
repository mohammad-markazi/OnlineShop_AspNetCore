using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Application;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Domain.CommentAgg.ApplicationContacts;

namespace CommentManagement.Application
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

            var comment = new Comment(command.Name, command.Email, command.Message, command.EntityId,command.EntityType,command.ParentId);

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
          
            return _commentRepository.Search(model);
        }


    }
}
