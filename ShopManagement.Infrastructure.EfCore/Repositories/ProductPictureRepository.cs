using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
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

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel command)
        {
            var query = _shopContext.ProductPictures.AsNoTracking();

            if (command.ProductId != 0)
                query = query.Where(x => x.ProductId == command.ProductId);

            var result = query.Include(x => x.Product).Select(x => new ProductPictureViewModel()
            {
                CreationDate = x.CreationDate.ToFarsi(),
                Id = x.Id,
                Picture = x.Picture,
                ProductName = x.Product.Name,
                IsRemoved = x.IsRemoved
            });
            return result.OrderByDescending(x => x.Id).ToList();
        }
    }
}
