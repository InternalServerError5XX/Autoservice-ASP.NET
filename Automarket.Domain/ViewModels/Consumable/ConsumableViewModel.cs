using Automarket.Domain.Enum.ConsumableType;
using Automarket.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Automarket.Domain.Helpers;
using Microsoft.AspNetCore.Http;

namespace Automarket.Domain.ViewModels.ConsumableViewModel
{
    public class ConsumableViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [EnumDataType(typeof(ConsumableCategory), ErrorMessage = "Invalid category.")]
        public ConsumableCategory ConsumableType { get; set; }

        [Required(ErrorMessage = "Subcategory is required")]
        [EnumDataType(typeof(ConsumableSubcategory), ErrorMessage = "Invalid subcategory.")]
        public ConsumableSubcategory Subcategory { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [MinLength(2, ErrorMessage = "Brand must be greater than 1")]
        [MaxLength(30, ErrorMessage = "Brand must be less than 30")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [MinLength(2, ErrorMessage = "Model must be greater than 1")]
        [MaxLength(50, ErrorMessage = "Model must be less than 50")]
        public string Model { get; set; }

        [MinLength(2, ErrorMessage = "Country must be greater than 1")]
        [MaxLength(30, ErrorMessage = "Country must be less than 30")]
        public string? Country { get; set; }

        [DynamicRange(2000, 2100)]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000000, ErrorMessage = "Invalid price value")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, 1000000, ErrorMessage = "Invalid quantity value")]
        public int Quantity { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EditDate { get; set; }       

        [PhotoValidation(ErrorMessage = "Photo is required")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", ".svg" }, ErrorMessage = "Invalid format")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "Maximum file size is 5 MB")]
        public IFormFile? Photo { get; set; }

        public string? PhotoPath { get; set; }
    }
}
