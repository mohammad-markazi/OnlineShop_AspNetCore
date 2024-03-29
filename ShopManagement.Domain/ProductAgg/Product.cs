﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Domain.ProductAgg
{
    public class Product:EntityBase
    {
            public string Code { get; private set; }

            public string Name { get;private set; }

            public string ShortDescription { get; private set; }
            public string Description { get; private set; }
            public string Picture { get; private set; }
            public string PictureAlt { get; private set; }
            public string PictureTitle { get; private set; }
            public string Slug { get; private set; }
            public string Keywords { get; private set; }
            public string MetaDescription { get; private set; }
            public long CategoryId { get; private set; }
        public ProductCategory Category { get; set; }
        public List<ProductPicture> ProductPictures { get;private set; }
        public Product()
        {
            ProductPictures=new List<ProductPicture>();
        }
        public Product(string code, string name, string shortDescription, string description, string picture, string pictureAlt, string pictureTitle, string slug, string keywords, string metaDescription, long categoryId)
        {
            Code = code;
            Name = name;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
        }

      public  void Edit(string code, string name, string shortDescription, string description,
            string picture, string pictureAlt, string pictureTitle, string slug, string keywords,
            string metaDescription, long categoryId)
        {
            Code = code;
            Name = name;
            ShortDescription = shortDescription;
            Description = description;
            if(!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            CategoryId = categoryId;
        }

    }
}
