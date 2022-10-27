using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.Order;

namespace ShopManagement.Application
{
    public class CartService:ICartService
    {
        public Cart Cart { get; set; }
        public void SetCart(Cart cart)
        {
            Cart = cart;
        }

        public Cart GetCart()
        {
            return Cart;
        }
    }
}
