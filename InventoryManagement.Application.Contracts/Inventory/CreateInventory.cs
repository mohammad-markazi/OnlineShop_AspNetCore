using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Contracts.Inventory
{
    public class CreateInventory
    {
        public long EntityId { get; set; }

        public double UnitPrice { get; set; }

        public Dictionary<long,string> RelatedEntities { get; set; }
    }
}
