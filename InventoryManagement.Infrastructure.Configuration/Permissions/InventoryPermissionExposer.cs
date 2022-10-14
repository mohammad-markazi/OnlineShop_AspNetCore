using _0_Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Configuration.Permissions
{
    public class InventoryPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {"Inventory",new List<PermissionDto>(){
                new PermissionDto("ListInventory",InventoryPermissions.ListInventory),
                new PermissionDto("SearchInventory",InventoryPermissions.SearchInventory),
                new PermissionDto("CreateInventory",InventoryPermissions.CreateInventory),
                new PermissionDto("EditInventory",InventoryPermissions.EditInventory),
                new PermissionDto("IncreaseInventory",InventoryPermissions.IncreaseInventory),
                new PermissionDto("ReduceInventory",InventoryPermissions.ReduceInventory),
                new PermissionDto("LogInventory",InventoryPermissions.LogInventory),

            }
                },  
               
            };
        }
    }
}
