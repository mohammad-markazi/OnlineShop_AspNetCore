using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EfCore.Repositories
{
    public class ProductCategoryRepository:RepositoryBase<long,ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context):base(context)
        {
            _context = context;
        }

       
        public EditProductCategory GetDetails(long id)
        {
            return _context.ProductCategories.Select(x => new EditProductCategory()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                MetaDescription = x.MetaDescription,
                //Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Keywords = x.Keywords,
                Slug = x.Slug
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductCategoryViewModel> Search(SearchProductCategory command)
        {
            var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                Id = x.Id,
                Name = x.Name,  
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi()
            });
            if (!string.IsNullOrWhiteSpace(command.Name))
                query = query.Where(x => x.Name.Contains(command.Name));

            return query.OrderByDescending(x=>x.Id).ToList();
        }

    }
}
