using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ServiceHost.Areas.Administration.Pages.Shared;
using ShopManagement.Application.Contracts.Comment;

namespace ServiceHost.Areas.Administration.Pages.Shop.Comments
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
        public void OnGet()
        {
            Comments = _commentApplication.Search(Search);
        }



        public IActionResult OnGetConfirm(long id)
        {
            _commentApplication.Confirm(id);
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetCanceled(long id)
        {
            _commentApplication.Canceled(id);
            return RedirectToPage("./Index");
        }
    }
}
