using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Order
{
    public class OrderItemViewModel
    {
        public string ProductName { get;  set; }

        public long Count { get;  set; }

        public double UnitPrice { get;  set; }
        public int DiscountRate { get;  set; }

    }
}
