using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ShopManagement.Application.Contracts.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Code { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Name { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string ShortDescription { get;  set; }
        public string Description { get;  set; }
        [FileExtensionLimitation(new[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = ValidationMessage.ErrorExtensionFile)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.ErrorSize)]
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string Slug { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string Keywords { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string MetaDescription { get;  set; }
        [Range(1,10000,ErrorMessage = ValidationMessage.Required)]
        public long CategoryId { get;  set; }

        public List<ProductCategoryViewModel> Categories { get; set; }
    }
}
