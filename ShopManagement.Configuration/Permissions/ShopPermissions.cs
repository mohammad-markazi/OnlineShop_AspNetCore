using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Configuration.Permissions
{
    public static class ShopPermissions
    {
        public  const int ListProduct = 10;
        public const int SearchProduct = 11;
        public const int CreateProduct = 12;
        public const int EditProduct = 13;


        public const int ListProductCategory = 20;
        public const int SearchProductCategory = 21;
        public const int CreateProductCategory = 22;
        public const int EditProductCategory = 23;


        public const int ListProductPicture = 30;
        public const int SearchProductPicture = 31;
        public const int CreateProductPicture = 32;
        public const int EditProductPicture = 33;
        public const int RestoreProductPicture = 34;
        public const int RemoveProductPicture = 35;

        public const int ListSlide = 40;
        public const int CreateSlide = 41;
        public const int EditSlide = 42;
        public const int RestoreSlide = 43;
        public const int RemoveSlide = 44;


        public const int ListOrder = 410;

    }
}
