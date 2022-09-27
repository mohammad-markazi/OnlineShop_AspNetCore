using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Application.Contracts.Slide;

namespace ShopManagement.Application.Contracts.Slide
{
    public class CreateSlide
    {
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string Heading { get;  set; }
        public string Title { get;  set; }
        public string Text { get;  set; }
        public string BtnText { get;  set; }
        [Url(ErrorMessage = "مقدار وارد شده لینک معتبر نمیباشد")]
        public string Link { get; set; }
    }
}


