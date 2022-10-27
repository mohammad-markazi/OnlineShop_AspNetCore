using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using InventoryManagement.Application.Contracts.Inventory;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository:IRepository<long,Inventory>
    {
        Inventory GetByEntity(long entityId);

        Dictionary<long, string> GetProducts();

        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);
    }
}
