using _01_LampShadeQuery.Contracts.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using _01_LampShadeQuery.Contracts;

namespace ServiceHost.Pages
{
    public class CheckoutModel : PageModel
    {
        public const string CookieName = "cart-items";
        private readonly ICartCalculatorService _cartCalculatorService;

        public CheckoutModel(ICartCalculatorService cartCalculatorService)
        {
            _cartCalculatorService = cartCalculatorService;
        }
        public Cart Cart { get; set; }
        public void OnGet()
        {
            var value = Request.Cookies[CookieName];

            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(value);

            Cart = _cartCalculatorService.ComputeCart(cartItems);
        }
    }
}
