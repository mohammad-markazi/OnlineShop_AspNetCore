namespace InventoryManagement.Application.Contracts.Inventory
{
    public class InventorySearchModel
    {
        public long EntityId { get; set; }
        public bool NotInStock { get; set; }

}
}