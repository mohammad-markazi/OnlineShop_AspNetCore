using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class OrderRepository:RepositoryBase<long,Order>,IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly AccountContext _accountContext;
        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public double GetAmountBy(long id)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (order is null)
                return 0;

            return order.PayAmount;
        }

        public List<OrderViewModel> Search(OrderSearchModel model)
        {
            var query = _context.Orders.Select(x=>new OrderViewModel()
            {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                PayAmount = x.PayAmount,
                Status = (int)x.Status,
                TotalAmount = x.TotalAmount,
                IssueTrackingNo = x.IssueTrackingNo,
                RefId = x.RefId,
                CreationDate = x.CreationDate.ToFarsi(),
            });

            if(model.AccountId!=0)
                query=query.Where(x=>x.AccountId==model.AccountId);

            if (model.Status != -1)
                query = query.Where(x => x.Status == model.Status);

            var result= query.OrderByDescending(x => x.Id).ToList();

            foreach (var item in result)
                item.AccountFullName = _accountContext.Accounts.Single(x => x.Id == item.AccountId).FullName;

            return result;
        }

        public List<OrderItemViewModel> GetOrderItemsBy(long orderId)
        {
            var order = _context.Orders.Include(x=>x.Items).ThenInclude(x=>x.Product).FirstOrDefault(x => x.Id == orderId);
            if (order is null)
                return new List<OrderItemViewModel>();

            return order.Items.Select(x => new OrderItemViewModel()
            {
                Count = x.Count,
                DiscountRate = x.DiscountRate,
                UnitPrice = x.UnitPrice,
                ProductName = x.Product.Name
            }).ToList();
        }
    }
}
