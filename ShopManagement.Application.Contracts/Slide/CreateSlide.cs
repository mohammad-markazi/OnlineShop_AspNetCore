using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.Slide;

namespace ShopManagement.Application.Contracts.Slide
{
    public class CreateSlide
    {
        [FileExtensionLimitation(new[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = ValidationMessage.ErrorExtensionFile)]
        [MaxFileSize(3 * 1024 * 1024, ErrorMessage = ValidationMessage.ErrorSize)]
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Heading { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Title { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Text { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string BtnText { get;  set; }
        [Url(ErrorMessage = "مقدار وارد شده لینک معتبر نمیباشد")]
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Link { get; set; }
    }
}


