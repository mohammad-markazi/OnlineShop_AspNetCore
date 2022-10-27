using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.Order
{
    public interface IOrderApplication
    {
        long PlaceOrder(Cart cart);

        string PaymentSucceeded(long orderId,long refId);
        double GetAmountBy(long id);
        List<OrderViewModel> Search(OrderSearchModel model);

        void Canceled(long id);

        List<OrderItemViewModel> GetOrderItemsBy(long orderId);
    }
}
