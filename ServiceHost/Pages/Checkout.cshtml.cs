using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Infrastructure.ZarinPal;
using _01_LampShadeQuery.Contracts;
using _01_LampShadeQuery.Contracts.Product;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Authorization;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ServiceHost.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public const string CookieName = "cart-items";
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IOrderApplication _orderApplication;
        private readonly IZarinPalFactory _zarinPalFactory;
        public CheckoutModel(ICartCalculatorService cartCalculatorService, ICartService cartService, IProductQuery productQuery, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory)
        {
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
        }
        public Cart Cart { get; set; }
        public void OnGet()
        {
            var value = Request.Cookies[CookieName];

            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(value);

            Cart = _cartCalculatorService.ComputeCart(cartItems);

            _cartService.SetCart(Cart);
        }

        public async  Task<IActionResult> OnGetPay()
        {
            Cart = _cartService.GetCart();
            if (_productQuery.CheckInventory(Cart))
                return RedirectToPage("./Cart");
            var orderId = _orderApplication.PlaceOrder(Cart);

          var paymentResponse= await _zarinPalFactory.CreatePaymentRequest(Cart.PayAmount.ToString(), "", "", "خرید از سایت", orderId);

          return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
        }

        public async Task<IActionResult> OnGetCallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
        {
            var amount = _orderApplication.GetAmountBy(oId);

            var result =await _zarinPalFactory.CreateVerificationRequest(authority, amount.ToString());
            var paymentResult = new PaymentResult();
            if (status == "OK" && result.Status == 100)
            {
               var issueTrackingNo= _orderApplication.PaymentSucceeded(oId,result.RefID);
                Response.Cookies.Delete(CookieName);
                 paymentResult = new PaymentResult()
                {
                    IsSuccessful = true,
                    Message = "پرداخت با موفقیت انجام شد",
                    IssueTrackingNo = issueTrackingNo
                };

            }
            else
            {
                paymentResult = new PaymentResult()
                {
                    IsSuccessful = false,
                    Message = "پرداخت با شکست مواجه شد در صورت کسر وجه از جساب شما مبلغ تا 24 ساعت دیگر به شما عودت خواهد شد",
                    IssueTrackingNo = null
                };
            }
            return RedirectToPage("./PaymentResult", paymentResult);

        }

    }
}
