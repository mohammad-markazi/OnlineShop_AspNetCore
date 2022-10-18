using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampShadeQuery.Contracts.Order
{
    public class Cart
    {
        public List<CartItem> CartItems { get; set; }
        public double TotalAmount { get; set; }
        public string DiscountAmount { get; set; }

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
    public class CartItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public double TotalPrice => Price * Count;
        public int DiscountRate { get; set; }
        public double DiscountAmount { get; set; }
        public double PayAmount { get; set; }

        public bool InStock { get; set; }


    }
}
