using Automarket.Domain.ViewModels.Account;
using Automarket.Domain.ViewModels.ConsumableViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Helpers
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ValidationHelper : RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (AccountViewModel)validationContext.ObjectInstance;

            if (model.Id == 0)
            {
                return base.IsValid(value, validationContext);
            }

            return ValidationResult.Success;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PhotoValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (ConsumableViewModel)validationContext.ObjectInstance;

            if (model.Id == 0)
            {
                if (model.Photo == null)
                {
                    return new ValidationResult("Photo is required");
                }
            }

            return ValidationResult.Success;
        }
    }


    public class DynamicRangeAttribute : ValidationAttribute
    {
        private int _min;
        private int _max;

        public DynamicRangeAttribute(int min, int max)
        {
            _min = min;
            _max = DateTime.Now.Year + 2;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is int val)
            {
                if (val < _min || val > _max)
                {
                    return new ValidationResult("Year must be between " + _min + " - " + _max);
                }
                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid value type for Year");
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Allowed file extensions are: {string.Join(", ", _extensions)}";
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        public MaxFileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Maximum allowed file size is {_maxFileSize / (1024 * 1024)} MB.";
        }
    }
}
