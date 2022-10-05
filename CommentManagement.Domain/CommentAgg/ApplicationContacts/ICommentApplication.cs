using System.Collections.Generic;
using _0_Framework.Application;
using CommentManagement.Domain.CommentAgg.ApplicationContacts;

namespace CommentManagement.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        void Confirm(long id);
        void Canceled(long id);
        List<CommentViewModel> Search(CommentSearchModel model);

    }
}
