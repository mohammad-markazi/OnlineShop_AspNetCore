using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.Order
{
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
