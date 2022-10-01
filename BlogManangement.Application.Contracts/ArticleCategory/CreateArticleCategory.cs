using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Name { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public IFormFile Picture { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string PictureTitle { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string Description { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public int ShowOrder { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string Slug { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string Keywords { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string MetaDescription { get;  set; }
        public string CanonicalAddress { get;  set; }
    }
}
