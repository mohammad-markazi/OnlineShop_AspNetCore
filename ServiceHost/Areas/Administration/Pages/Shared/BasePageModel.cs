using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Shared
{
    public class BasePageModel:PageModel
    {
        [TempData]
        public string Notification { get; set; }
        public void Alert(string message, NotificationType notificationType)
        {
            message = $"'{message}'";
            var titleMessage = "";
            if (notificationType == NotificationType.Error)
            {
                titleMessage = "'خطا'";
            }

            if (notificationType == NotificationType.Warning)
            {
                titleMessage = "'هشدار'";

            }
            if (notificationType == NotificationType.Success)
            {
                titleMessage = "'موفق'";
            }

            var notifType =$"'{notificationType.ToString().ToLower()}'";
            //var msg = "swal('" + titleMessage + "', '" + message + "','" + notificationType + "')" + "";
            var msg = "swal({title:"+titleMessage+",text: "+ message +",timer: 3000,type: "+ notifType + ",showCancelButton: false,showConfirmButton: false })";
            Notification = msg;
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
