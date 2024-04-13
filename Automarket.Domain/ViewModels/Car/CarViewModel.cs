using Automarket.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.ViewModels.Car
{
    public class CarViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Enter the name")]
        [MinLength(1, ErrorMessage = "The length must be greater than 0")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter the model")]
        [MinLength(1, ErrorMessage = "The length must be greater than 0")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Enter the description")]
        [MinLength(1, ErrorMessage = "The length must be greater than 0")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter the speed")]
        [Range(100, 400, ErrorMessage = "Speed must be 100 < x < 400")]
        public int Speed { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Enter the date")]
        [Range(5000, 200000, ErrorMessage = "Speed must be 5000 < x < 200000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Enter the type")]
        public string CarType { get; set; }
    }
}
