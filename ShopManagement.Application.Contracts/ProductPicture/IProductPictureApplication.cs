using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);

        OperationResult Edit(EditProductPicture command);

        EditProductPicture GetDetail(long id);
        OperationResult Remove(long id);
        OperationResult Restore(long id);

        List<ProductPictureViewModel> Search(ProductPictureSearchModel command);
    }
}
