namespace ShopManagement.Application.Contracts.Product
{
    public class SearchProductModel
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public long CategoryId { get; set; }
    }
}