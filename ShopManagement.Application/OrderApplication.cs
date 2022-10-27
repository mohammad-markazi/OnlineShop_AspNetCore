using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application
{
    public class OrderApplication:IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IShopInventoryAcl _shopInventoryAcl;
        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IShopInventoryAcl shopInventoryAcl)
        {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _shopInventoryAcl = shopInventoryAcl;
        }

        public long PlaceOrder(Cart cart)
        {
            var accountId = _authHelper.GetUserInfo().AccountId;
            var issueTrackingNo = CodeGenerator.Generate(DateTime.Now.Year.ToString());

            var order = new Order(accountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount);
            foreach (var item in cart.CartItems)
            {
                var orderItem = new OrderItem(item.Id, item.Count, item.Price, item.DiscountRate);
                order.AddItem(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();

            return order.Id;
        }

        public string PaymentSucceeded(long orderId, long refId)
        {
            var order = _orderRepository.Get(orderId);
            order.PaymentSucceeded(refId);
            var issueTrackingNo = CodeGenerator.Generate("B");
            order.SetIssueTrackingNo(issueTrackingNo);
            if (_shopInventoryAcl.ReduceFromInventory(order.Items))
            {
                _orderRepository.SaveChanges();
                return issueTrackingNo;
            }

            return String.Empty;
        }

        public double GetAmountBy(long id)
        {
            return _orderRepository.GetAmountBy(id);
        }

        public List<OrderViewModel> Search(OrderSearchModel model)
        {
            return _orderRepository.Search(model);
        }

        public void Canceled(long id)
        {
            var order = _orderRepository.Get(id);
            order.Canceled();

            _orderRepository.SaveChanges();
        }

        public List<OrderItemViewModel> GetOrderItemsBy(long orderId)
        {
            return _orderRepository.GetOrderItemsBy(orderId);
        }
    }
}
