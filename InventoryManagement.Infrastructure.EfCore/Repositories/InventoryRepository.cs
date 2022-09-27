using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EfCore;

namespace InventoryManagement.Infrastructure.EfCore.Repositories
{
    public class InventoryRepository:RepositoryBase<long,Inventory>,IInventoryRepository
    {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;
        public InventoryRepository(InventoryContext context, ShopContext shopContext) : base(context)
        {
            _context = context;
            _shopContext = shopContext;
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
    }
}
