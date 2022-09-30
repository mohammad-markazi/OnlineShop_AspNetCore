using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampShadeQuery.Contracts.Product
{
    public class ProductQueryModel
    {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string ShortDescription { get; set; }
        public string PriceWithDiscount { get; set; }
        public int DiscountRate { get; set; }
        public string CategoryName { get; set; }
        public string Picture { get; set; }
        public string PictureTitle { get; set; }
        public string PictureAlt { get; set; }
        public string Slug { get; set; }
        public string DiscountExpiration { get; set; }
        public string Code { get;  set; }
        public string Description { get;  set; }
        public string Keywords { get;  set; }
        public string MetaDescription { get;  set; }
        public string CategorySlug { get; set; }
        public bool IsInStock { get; set; }

        public List<ProductPictureQueryModel> ProductPictures { get; set; }
        public List<CommentQueryModel> Comments { get; set; }

    }

    public class CommentQueryModel
    {
        public string Name { get; set; }
        public string Message { get; set; }

    }
    public class ProductPictureQueryModel
    {
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public bool IsRemoved { get; set; }
    }
}
