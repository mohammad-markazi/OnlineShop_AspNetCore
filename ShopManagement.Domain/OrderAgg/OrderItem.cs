using _0_Framework.Domain;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Domain.OrderAgg
{
    public class OrderItem:EntityBase
    {
        public long ProductId { get;private set; }

        public long OrderId { get; private set; }
        public long Count { get; private set; }

        public double UnitPrice { get; private set; }
        public int DiscountRate { get; private set; }

        public Product Product { get; set; }

        public Order Order { get; set; }

        public OrderItem(long productId, long count, double unitPrice, int discountRate)
        {
            ProductId = productId;
            Count = count;
            UnitPrice = unitPrice;
            DiscountRate = discountRate;
        }
    }
}