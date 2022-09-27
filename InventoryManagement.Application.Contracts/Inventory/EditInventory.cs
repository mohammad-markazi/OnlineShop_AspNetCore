using System.Collections.Generic;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class EditInventory:CreateInventory{
        public long Id { get; set; }
        public Dictionary<long, string> RelatedEntities { get; set; }

    }
}