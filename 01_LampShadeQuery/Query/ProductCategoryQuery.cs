using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.Product;
using _01_LampShadeQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _01_LampShadeQuery.Query
{
    public class ProductCategoryQuery:IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> ProductCategories()
        {
            return _context.ProductCategories.AsNoTracking().Select(x => new ProductCategoryQueryModel()
            {
                Id = x.Id,
                Picture = x.Picture,
                Name = x.Name,
                PictureTitle = x.PictureTitle,
                PictureAlt = x.PictureAlt,
                Slug = x.Slug
            }).ToList();
        }

        public List<ProductCategoryQueryModel> ProductCategoriesWithProducts()
        {
            return _context.ProductCategories.Include(x => x.Products).Select(x => new ProductCategoryQueryModel()
            {
                Id = x.Id,
                Name = x.Name,
                Products =MapProducts(x.Products,_inventoryContext,_discountContext)
            }).ToList();
        }

        public ProductCategoryQueryModel ProductCategoriesWithProductsBy(string slug)
        {
            return _context.ProductCategories.Include(x => x.Products).Select(x => new ProductCategoryQueryModel()
            {
                Id = x.Id,
                Name = x.Name,
                MetaDescription = x.MetaDescription,
                Keywords = x.Keywords,
                Description = x.Description,
                Slug = x.Slug,
                Products = MapProducts(x.Products, _inventoryContext, _discountContext)
            }).FirstOrDefault(x=>x.Slug==slug);
        }

        private static List<ProductQueryModel> MapProducts(List<Product> products,InventoryContext inventoryContext,DiscountContext discountContext)
        {

            var result=new List<ProductQueryModel>();
            foreach (var product in products)
            {
                var inventory = inventoryContext.Inventory.FirstOrDefault(x => x.EntityId == product.Id);
                var discount = discountContext.CustomerDiscounts
                    .FirstOrDefault(x => x.EndDate > DateTime.Now && x.StartDate < DateTime.Now && x.ProductId==product.Id);

                var productQueryModel = new ProductQueryModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Picture = product.Picture,
                    PictureTitle = product.PictureTitle,
                    PictureAlt = product.PictureAlt,
                    Price = inventory is null? "بدون قیمت گذاری":inventory.UnitPrice.ToMoney(),
                    PriceDouble = inventory?.UnitPrice ?? 0,
                    DiscountRate = discount?.DiscountRate ?? 0,
                    Slug = product.Slug
                };
                if(productQueryModel.DiscountRate!=0 && inventory is not null)
                {
                    var discountAmout = Math.Round((inventory.UnitPrice * productQueryModel.DiscountRate) / 100);
                    productQueryModel.PriceWithDiscount=(inventory.UnitPrice - discountAmout).ToMoney();
                    productQueryModel.PriceWithDiscountDouble = (inventory.UnitPrice - discountAmout);
                    productQueryModel.DiscountExpiration = discount!.EndDate.ToDiscountFormat();
                }
                result.Add(productQueryModel);

            }
            return result;
        }
    }
}
