using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository:IRepository<long,ProductPicture>
    {
        ProductPicture GetWithProductAndCategoryBy(long id);
    }
}
