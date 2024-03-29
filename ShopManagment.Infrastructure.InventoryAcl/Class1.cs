﻿using System;
using System.Collections.Generic;
using InventoryManagement.Application;
using InventoryManagement.Application.Contracts.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl:IShopInventoryAcl
    {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduceFromInventory(List<OrderItem> items)
        {
            var command = new List<ReduceInventory>();
            foreach (var orderItem in items)
            {
                var item = new ReduceInventory()
                {
                    EntityId = orderItem.ProductId,
                    OrderId = orderItem.OrderId,
                    Count = orderItem.Count,
                    Description = "خرید از سایت",
                };
                command.Add(item);
            }

            return _inventoryApplication.Reduce(command).IsSuccess;
        }
    }
}
