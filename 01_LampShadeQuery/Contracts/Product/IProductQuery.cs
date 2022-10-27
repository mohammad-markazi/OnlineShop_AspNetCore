using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.Order;

namespace _01_LampShadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestProducts();
        List<ProductQueryModel> Search(string value);

        ProductQueryModel GetDetailBy(string slug);

        List<CartItem> CheckStatusInventory( List<CartItem> cartItems);

        bool CheckInventory(Cart cart);
    }
}
