using System.Collections.Generic;
using _0_Framework.Application;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public interface IInventoryApplication
    {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        EditInventory GetDetail(long id);
        List<InventoryViewModel> Search(InventorySearchModel model);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Reduce(List<ReduceInventory> command);
        OperationResult Reduce(ReduceInventory command);

        List<InventoryOperationViewModel> GetOperationLog(long inventoryId);


    }
}