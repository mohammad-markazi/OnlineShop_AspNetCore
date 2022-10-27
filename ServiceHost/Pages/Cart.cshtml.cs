using System;
using System.Collections.Generic;
using System.Linq;
using _01_LampShadeQuery.Contracts.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShopManagement.Application.Contracts.Order;

namespace ServiceHost.Pages
{
    public class CartModel : PageModel
    {
        private readonly IProductQuery _productQuery;

        public CartModel(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }
        public const string CookieName = "cart-items";
        public List<CartItem> CartItems { get; set; }

        public void OnGet()
        {
            var cartsCookie = Request.Cookies[CookieName];

            if(cartsCookie is null)
                CartItems=new List<CartItem>();
            else
            {
                CartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartsCookie);
                CartItems = _productQuery.CheckStatusInventory(CartItems);
            }
           
        }

        public IActionResult OnGetRemoveItemCart(long id)
        {
            var value = Request.Cookies[CookieName];

            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(value);
            Response.Cookies.Delete(CookieName);
            var removeItem = cartItems.FirstOrDefault(x => x.Id == id);
            cartItems.Remove(removeItem);

            var option = new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddDays(2),
                Path = "/"
            };

            Response.Cookies.Append(CookieName, JsonConvert.SerializeObject(cartItems),option);

            return RedirectToPage("./Cart");

        }

        public IActionResult OnGetGoToCheckOut()
        {
            var value = Request.Cookies[CookieName];

            var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(value);
            CartItems = _productQuery.CheckStatusInventory(cartItems);
            if(CartItems.Any(x=>!x.InStock))
                return RedirectToPage("./Cart");

                return RedirectToPage("./Checkout");

        }
    }
}
