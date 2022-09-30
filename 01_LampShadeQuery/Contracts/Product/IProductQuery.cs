using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampShadeQuery.Contracts.Product
{
    public interface IProductQuery
    {
        List<ProductQueryModel> GetLatestProducts();
        List<ProductQueryModel> Search(string value);

        ProductQueryModel GetDetailBy(string slug);
    }
}
