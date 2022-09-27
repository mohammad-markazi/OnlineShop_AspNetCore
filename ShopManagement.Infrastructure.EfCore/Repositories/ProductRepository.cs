using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;
        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductViewModel> Search(SearchProductModel model)
        {
            var query = _context.Products.Include(x=>x.Category).Select(x => new ProductViewModel()
            {
                CategoryName = x.Category.Name,
                Name = x.Name,
                Code = x.Code,
                CategoryId=x.CategoryId,
                Id = x.Id,
                CreationDate = x.CreationDate.ToFarsi(),
                Picture = x.Picture,
            });
            if (!string.IsNullOrWhiteSpace(model.Name))
                query = query.Where(x => x.Name.Contains(model.Name));
            if(!string.IsNullOrWhiteSpace(model.Code))
                query = query.Where(x => x.Code==model.Code);
            if (model.CategoryId != 0)
                query = query.Where(x => x.CategoryId == model.CategoryId);

            return query.OrderByDescending(x=>x.Id).ToList();
        }

        public EditProduct GetDetail(long id)
        {
            return _context.Products.Select(x => new EditProduct()
            {
                CategoryId = x.CategoryId,
                Code = x.Code,
                Description = x.Description,
                Id = x.Id,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
            }).FirstOrDefault(x => x.Id == id);
        }
    }
}
