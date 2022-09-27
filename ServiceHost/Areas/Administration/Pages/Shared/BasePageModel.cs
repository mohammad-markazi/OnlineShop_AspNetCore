using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Shared
{
    public class BasePageModel:PageModel
    {

        public void Alert(string message, NotificationType notificationType)
        {
            var titleMessage = "";
            if (notificationType == NotificationType.Error)
            {
                titleMessage = "خطا";
            }

            if (notificationType == NotificationType.Warning)
            {
                titleMessage = "هشدار";

            }
            if (notificationType == NotificationType.Success)
            {
                titleMessage = "موفق";
            }


            //var msg = "swal('" + titleMessage + "', '" + message + "','" + notificationType + "')" + "";
            var msg = "swal('" + titleMessage + "', '" + message + "','" + notificationType + "', {buttons: false,timer: 100000,});";
            TempData["notification"] = msg;
        }
        public enum NotificationType
        {
            Error,
            Success,
            Warning,
            Info
        }
    }
}
