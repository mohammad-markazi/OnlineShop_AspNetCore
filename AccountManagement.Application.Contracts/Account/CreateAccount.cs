using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account
{
    public class CreateAccount
    {
        public string FullName { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]
        public string Username { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string Password { get;  set; }
        [Required(ErrorMessage = ValidationMessage.Required)]

        public string Mobile { get;  set; }
        [FileExtensionLimitation(new[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = ValidationMessage.ErrorExtensionFile)]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = ValidationMessage.ErrorSize)]
        public IFormFile Profile { get;  set; }
        [Range(1,int.MaxValue,ErrorMessage = ValidationMessage.Required)]
        public long RoleId { get;  set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
