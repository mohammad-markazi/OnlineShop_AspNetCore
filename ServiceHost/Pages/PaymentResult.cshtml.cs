using _0_Framework.Infrastructure.ZarinPal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class PaymentResultModel : PageModel
    {
        public PaymentResult PaymentResult { get; set; }
        public void OnGet(PaymentResult result)
        {
            PaymentResult = result;
        }
    }
}
