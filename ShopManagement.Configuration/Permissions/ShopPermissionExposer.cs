using _0_Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Configuration.Permissions
{
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {"Products",new List<PermissionDto>(){
                new PermissionDto("CreateProduct",ShopPermissions.CreateProduct),
                new PermissionDto("ListProducts",ShopPermissions.ListProduct),
                new PermissionDto("SearchProducts",ShopPermissions.SearchProduct),
                new PermissionDto("EditProduct",ShopPermissions.EditProduct),

                }
                },
                {"ProductCategory",new List<PermissionDto>(){
                new PermissionDto("CreateProductCategory",ShopPermissions.CreateProductCategory),
                new PermissionDto("ListProductCategory",ShopPermissions.ListProductCategory),
                new PermissionDto("SearchProductCategory",ShopPermissions.SearchProductCategory),
                new PermissionDto("EditProductCategory",ShopPermissions.EditProductCategory),

                }
                },
                {"ProductPicture",new List<PermissionDto>(){
                        new PermissionDto("CreateProductPicture",ShopPermissions.CreateProductPicture),
                        new PermissionDto("ListProductPicture",ShopPermissions.ListProductPicture),
                        new PermissionDto("SearchProductPicture",ShopPermissions.SearchProductPicture),
                        new PermissionDto("EditProductPicture",ShopPermissions.EditProductPicture),

                    }
                },

                {"Slide",new List<PermissionDto>(){
                        new PermissionDto("CreateSlide",ShopPermissions.CreateSlide),
                        new PermissionDto("ListSlide",ShopPermissions.ListSlide),
                        new PermissionDto("EditSlide",ShopPermissions.EditSlide),
                        new PermissionDto("RestoreSlide",ShopPermissions.RestoreSlide),
                        new PermissionDto("RemoveSlide",ShopPermissions.RemoveSlide),

                    }
                },
            };
        }
    }
}
