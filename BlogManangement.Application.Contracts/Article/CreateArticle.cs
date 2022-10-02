using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.Article
{
    public class CreateArticle
    {
        [Required(ErrorMessage =ValidationMessage.Required)]
        public string Title { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string ShortDescription { get;  set; }

        public string Description { get;  set; }
        [FileExtensionLimitation(new[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = ValidationMessage.ErrorExtensionFile)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.ErrorSize)]
        public IFormFile Picture { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string PictureTitle { get;  set; }

        public string Keywords { get;  set; }

        public string MetaDescription { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string Slug { get;  set; }

        public string CanonicalAddress { get;  set; }
[Range(1,1000000,ErrorMessage = ValidationMessage.Required)]        
        public long CategoryId { get;  set; }
        public string PublishDate { get;  set; }
    }
}
