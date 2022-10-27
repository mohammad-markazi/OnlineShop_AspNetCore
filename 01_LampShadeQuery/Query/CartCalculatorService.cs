using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Domain;
using _01_LampShadeQuery.Contracts;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using ShopManagement.Application.Contracts.Order;
using ShopManagement.Infrastructure.EfCore;

namespace _01_LampShadeQuery.Query
{
    public class CartCalculatorService : ICartCalculatorService
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly IAuthHelper _authHelper;
        public CartCalculatorService(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, IAuthHelper authHelper)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _authHelper = authHelper;
        }

        public Cart ComputeCart(List<CartItem> cartItems)
        {
            var cart = new Cart();
            var accountInfo = _authHelper.GetUserInfo();


            foreach (var item in cartItems)
            {

                if (accountInfo.Type == UserTypes.Colleague)
                {
                    var discount = _discountContext.ColleagueDiscounts.FirstOrDefault(x => x.ProductId == item.Id && !x.IsRemoved);
                    if (discount is not null) 
                        item.DiscountRate = discount.DiscountRate;
                }
                else
                {
                    var discount = _discountContext.CustomerDiscounts.FirstOrDefault(x => x.ProductId == item.Id && x.EndDate > DateTime.Now && x.StartDate < DateTime.Now);
                    if (discount is not null)
                        item.DiscountRate = discount.DiscountRate;
                }

                item.DiscountAmount = ((item.DiscountRate * item.TotalPrice) / 100);
                item.PayAmount = item.TotalPrice - item.DiscountAmount;
                cart.Add(item);
            }

          
            return cart;
        }
    }
}