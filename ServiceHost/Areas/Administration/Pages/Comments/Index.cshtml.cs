using System.Collections.Generic;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg.ApplicationContacts;
using CommentManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Areas.Administration.Pages.Shared;

namespace ServiceHost.Areas.Administration.Pages.Comments
{
    public class IndexModel :BasePageModel
    {
        private readonly ICommentApplication _commentApplication;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }
        public List<CommentViewModel> Comments { get; set; }
        [BindProperty(SupportsGet = true)]
        public CommentSearchModel Search { get; set; }
        [NeedPermission(CommentPermissions.ListComment)]
        public void OnGet()
        {
            Comments = _commentApplication.Search(Search);
        }


        [NeedPermission(CommentPermissions.ConfirmComment)]

        public IActionResult OnGetConfirm(long id)
        {
            _commentApplication.Confirm(id);
            return RedirectToPage("./Index");
        }
        [NeedPermission(CommentPermissions.CanceledComment)]

        public IActionResult OnGetCanceled(long id)
        {
            _commentApplication.Canceled(id);
            return RedirectToPage("./Index");
        }
    }
}
