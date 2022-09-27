using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace ShopManagement.Application.Contracts.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage =ValidationMessage.Required )]
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Slug { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Keywords { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string MetaDescription { get; set; }
    }
}
