using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EfCore;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EfCore;

namespace InventoryManagement.Infrastructure.EfCore.Repositories
{
    public class InventoryRepository:RepositoryBase<long,Inventory>,IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;
        public InventoryRepository(InventoryContext context, ShopContext shopContext, AccountContext accountContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public Inventory GetByEntity(long entityId)
        {
            return _context.Inventory.FirstOrDefault(x => x.EntityId == entityId);
        }

        public Dictionary<long, string> GetProducts()
        {
            return _shopContext.Products.Select(x => new {Id= x.Id,Name= x.Name }).ToList()
                .ToDictionary(x => x.Id, x => x.Name);
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            var inventory = _context.Inventory.FirstOrDefault(x=>x.Id==inventoryId);
            if (inventory is null)
                return new List<InventoryOperationViewModel>();

            var result= inventory.InventoryOperations.Select(x => new InventoryOperationViewModel()
            {
                Count = x.Count,
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                Id = x.Id,
                Operation = x.Operation,
                OrderId = x.OrderId,
                OperationDate = x.OperationDate.ToFarsi(),
                OperationId = x.OperationId,
            }).OrderByDescending(x => x.Id).ToList();
            foreach (var item in result)
            {
                item.Operator = _accountContext.Accounts.FirstOrDefault(x => x.Id == item.OperationId)?.FullName;
            }

            return result;
        }
    }
}
