using System.Collections.Generic;
using _0_Framework.Domain;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg.ApplicationContacts;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository:IRepository<long,Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel model);
    }
}
