using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_Framework.Application
{
    public class FileExtensionLimitationAttribute:ValidationAttribute,IClientModelValidator
    {
        private readonly string[] _validExtentions;

        public FileExtensionLimitationAttribute(string[] validExtentions)
        {
            _validExtentions = validExtentions;
        }

        public override bool IsValid(object value)
        {
            var file = value as IFormFile;
            if (file == null)
                return true;


            var fileExtension = Path.GetExtension(file.FileName);

            return _validExtentions.Contains(fileExtension);

        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val-fileExtensionLimit", ErrorMessage);
        }
    }
}
