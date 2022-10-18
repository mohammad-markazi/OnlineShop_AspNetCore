using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace InventoryManagement.Domain.InventoryAgg
{
    public class Inventory:EntityBase
    {
        public long EntityId { get;private set; }

        public double  UnitPrice{ get; private set; }

        public bool InStock { get; private set; }

        public List<InventoryOperation> InventoryOperations { get; private set; }
        [NotMapped]
        public long CurrentCount => CalculateCurrentCount();

        public Inventory(long entityId, double unitPrice)
        {
            EntityId = entityId;
            UnitPrice = unitPrice;
            InventoryOperations = new List<InventoryOperation>();
        }

        public void Edit(long entityId, double unitPrice)
        {
            EntityId = entityId;
            UnitPrice = unitPrice;
        }
        public long CalculateCurrentCount()
        {
            var plus = InventoryOperations.Where(x => x.Operation).Sum(x => x.Count);
            var minus= InventoryOperations.Where(x => !x.Operation).Sum(x => x.Count);
            return plus - minus;
        }

        public void Increase(long count,long operationId,string description)
        {
            var currentCount = CalculateCurrentCount() + count;
            var operation = new InventoryOperation(true, count, operationId, currentCount, description, 0, Id);
           this.InventoryOperations.Add(operation);
            InStock = currentCount > 0;
        }

        public void Reduce(long count, long operationId, long orderId, string description)
        {
            var currentCount = CalculateCurrentCount() - count;
            var operation = new InventoryOperation(false, count, operationId, currentCount, description, orderId, Id);
            this.InventoryOperations.Add(operation);
            InStock = currentCount > 0;

        }

    }
}
