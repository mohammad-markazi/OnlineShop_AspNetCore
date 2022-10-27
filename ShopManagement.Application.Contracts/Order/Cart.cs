using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Order
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountAmount { get; set; }  

        public double PayAmount { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }

        public void Add(CartItem item)
        {
            CartItems.Add(item);
            TotalAmount += item.TotalPrice;
            DiscountAmount += item.DiscountAmount;
            PayAmount += item.PayAmount;
        }
    }
}