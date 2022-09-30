using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Application.Contracts.ProductPicture
{
    public class CreateProductPicture
    {
        [Range(1, 10000, ErrorMessage = ValidationMessage.Required)]
        public long ProductId { get;  set; }
        [FileExtensionLimitation(new[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = ValidationMessage.ErrorExtensionFile)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.ErrorSize)]
        public IFormFile Picture { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string PictureTitle { get;  set; }

        public List<ProductViewModel>  Products { get; set; }
    }
}
