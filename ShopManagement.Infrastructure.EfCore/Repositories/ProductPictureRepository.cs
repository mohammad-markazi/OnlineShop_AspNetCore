using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class ProductPictureRepository:RepositoryBase<long,ProductPicture>,IProductPictureRepository
    {
        private readonly ShopContext _shopContext;
        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _shopContext = context;
        }

        public ProductPicture GetWithProductAndCategoryBy(long id)
        {
            return _shopContext.ProductPictures.Include(x => x.Product).ThenInclude(x => x.Category)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
