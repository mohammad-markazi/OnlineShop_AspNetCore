using InventoryManagement.Application.Contracts.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application
{
    public class InventoryApplication:IInventoryApplication
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create(CreateInventory command)
        {
           var operation=new OperationResult();

           if (_inventoryRepository.Exists(x => x.EntityId == command.EntityId))
               return operation.Failed(ApplicationMessages.DuplicatedRecord);
           var inventory = new Inventory(command.EntityId, command.UnitPrice);
           _inventoryRepository.Create(inventory);
           _inventoryRepository.SaveChanges();
           return operation.Succeeded();
        }

        public OperationResult Edit(EditInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.Id);
            if (inventory is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);

            if (_inventoryRepository.Exists(x =>x.Id!=command.Id && x.EntityId == command.EntityId))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            inventory.Edit(command.EntityId,command.UnitPrice);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditInventory GetDetail(long id)
        {
            var inventory=_inventoryRepository.Get(id);
            return new EditInventory()
            {
                EntityId = inventory.EntityId,
                Id = inventory.Id,
                UnitPrice = inventory.UnitPrice,
            };
        }

        public List<InventoryViewModel> Search(InventorySearchModel model)
        {
            var products = _inventoryRepository.GetProducts();

            var query = _inventoryRepository.GetAll();

            if (model.EntityId != 0)
                query = query.Where(x => x.EntityId == model.EntityId);

            if (model.NotInStock)
                query = query.Where(x => !x.InStock);

            var result = query.Select(x => new InventoryViewModel()
            {
                EntityId = x.EntityId,
                InStock = x.InStock,
                UnitPrice = x.UnitPrice,
                CurrentCount = x.CalculateCurrentCount(),
                Id = x.Id,
            }).ToList();

            result.ForEach(inventory=> inventory.EntityName=products.First(x=>x.Key==inventory.EntityId).Value);

            return result;
        }

        public OperationResult Increase(IncreaseInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);

            long operatorId = 1;

            inventory.Increase(command.Count,operatorId,command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(List<ReduceInventory> command)
        {
            long operatorId = 1;
            var operation = new OperationResult();

            foreach (var item in command)
            {
                var inventory = _inventoryRepository.GetByEntity(item.EntityId);
                inventory.Reduce(command.Count,operatorId,item.OrderId,item.Description);
            }
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Reduce(ReduceInventory command)
        {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.Get(command.InventoryId);
            if (inventory is null)
                return operation.Failed(ApplicationMessages.NotFoundRecord);

            long operatorId = 1;
            inventory.Reduce(command.Count, operatorId,0, command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<InventoryOperationViewModel> GetOperationLog(long inventoryId)
        {
            var inventory=_inventoryRepository.Get(inventoryId);

            return inventory.InventoryOperations.Select(x => new InventoryOperationViewModel()
            {
                Count = x.Count,
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                Id = x.Id,
                Operation = x.Operation,
                OrderId = x.OrderId,
                OperationDate = x.OperationDate.ToFarsi(),
                OperationId = x.OperationId,
                Operator = "مدیر سیستم"
            }).OrderByDescending(x=>x.Id).ToList();
        }
    }
}
