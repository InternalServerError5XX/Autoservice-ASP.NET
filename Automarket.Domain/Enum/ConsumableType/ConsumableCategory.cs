using Automarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Enum.ConsumableType
{
    public enum ConsumableCategory
    {
        [Display(Name = "Car Oils")]
        Car_Oils,

        [Display(Name = "Car Chemistry")]
        Car_Chemistry,

        [Display(Name = "Car Cosmetics")]
        Car_Cosmetics,
    }
}
