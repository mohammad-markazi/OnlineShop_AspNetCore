using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _01_LampShadeQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _01_LampShadeQuery.Query
{
    public class ProductQuery:IProductQuery
    {
        private readonly ShopContext _shopcontext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(ShopContext shopcontext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopcontext = shopcontext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestProducts()
        {
            var products = _shopcontext.Products.Include(x => x.Category).OrderByDescending(x => x.CreationDate).Take(6)
                .Select(x => new ProductQueryModel()
                {
                    CategoryName = x.Category.Name,
                    Id = x.Id,
                    Slug = x.Slug,
                    Name = x.Name,
                    Picture = x.Picture,
                    CategorySlug = x.Category.Slug,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                }).ToList();

            foreach (var product in products)
            {
                var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.EntityId == product.Id);
                var discount = _discountContext.CustomerDiscounts.FirstOrDefault(x => x.EndDate > DateTime.Now && x.StartDate < DateTime.Now && x.ProductId == product.Id);


                product.Price = inventory?.UnitPrice.ToMoney() ?? "بدون قیمت گذاری";

                product.DiscountRate = discount?.DiscountRate ?? 0;

                if (inventory != null && discount != null)
                    product.PriceWithDiscount =
                        (inventory.UnitPrice - Math.Round((inventory.UnitPrice * discount.DiscountRate) / 100))
                        .ToMoney();

            }
            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var products = _shopcontext.Products.Include(x => x.Category).OrderByDescending(x => x.CreationDate).Take(6)
                .Select(x => new ProductQueryModel()
                {
                    CategoryName = x.Category.Name,
                    Id = x.Id,
                    Slug = x.Slug,
                    Name = x.Name,
                    ShortDescription = x.ShortDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle
                }).Where(x=>x.Name.Contains(value)).OrderByDescending(x=>x.Id).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value))
                products = products.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

            foreach (var product in products)
            {
                var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.EntityId == product.Id);
                var discount = _discountContext.CustomerDiscounts.FirstOrDefault(x => x.EndDate > DateTime.Now && x.StartDate < DateTime.Now && x.ProductId == product.Id);


                product.Price = inventory?.UnitPrice.ToMoney() ?? "بدون قیمت گذاری";

                product.DiscountRate = discount?.DiscountRate ?? 0;

                if (inventory != null && discount != null)
                    product.PriceWithDiscount =
                        (inventory.UnitPrice - Math.Round((inventory.UnitPrice * discount.DiscountRate) / 100))
                        .ToMoney();

            }
            return products.ToList();
        }

        public ProductQueryModel GetDetailBy(string slug)
        {
            var product = _shopcontext.Products.Include(x => x.Category).Include(x=>x.ProductPictures).OrderByDescending(x => x.CreationDate).Take(6)
                .Select(x => new ProductQueryModel()
                {
                    CategoryName = x.Category.Name,
                    Id = x.Id,
                    Slug = x.Slug,
                    Name = x.Name,
                    ShortDescription = x.ShortDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    CategorySlug = x.Category.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Description = x.Description,
                    Code = x.Code,
                    ProductPictures = MapProductPictures(x.ProductPictures)
                }).FirstOrDefault(x=>x.Slug==slug);

            if (product == null)
                return new ProductQueryModel();
             var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.EntityId == product.Id);
                var discount = _discountContext.CustomerDiscounts.FirstOrDefault(x => x.EndDate > DateTime.Now && x.StartDate < DateTime.Now && x.ProductId == product.Id);
                

                product.Price = inventory?.UnitPrice.ToMoney() ?? "بدون قیمت گذاری";
                product.IsInStock = inventory?.InStock ?? false;
                product.DiscountRate = discount?.DiscountRate ?? 0;

                if (inventory != null && discount != null)
                {
                    product.PriceWithDiscount =
                        (inventory.UnitPrice - Math.Round((inventory.UnitPrice * discount.DiscountRate) / 100))
                        .ToMoney();
                product.DiscountExpiration = discount!.EndDate.ToDiscountFormat();
                }
        

                return product;

        }

        private static List<ProductPictureQueryModel> MapProductPictures(List<ProductPicture> productPictures)
        {
            return productPictures.Select(x => new ProductPictureQueryModel()
            {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                IsRemoved = x.IsRemoved
            }).ToList();
        }
    }
}
