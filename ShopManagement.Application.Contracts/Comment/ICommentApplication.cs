using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        OperationResult Add(AddComment command);
        void Confirm(long id);
        void Canceled(long id);
        List<CommentViewModel> Search(CommentSearchModel model);

    }
}
