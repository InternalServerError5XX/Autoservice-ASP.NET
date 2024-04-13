using Automarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Enum
{
    public enum ConsumableSubcategory
    {
        //Car_Oil subcategory
        [Display(Name = "Motor Oil")]
        Motor_Oil,

        [Display(Name = "Transmission Oil")]
        Transmission_Oil,

        [Display(Name = "Hydraulic Oil")]
        Hydraulic_Oil,

        [Display(Name = "Flushing Oil")]
        Flushing_Oil,


        //Car_Chemistry subcategory
        [Display(Name = "Glass Washer")]
        Glass_Washer,

        [Display(Name = "Antifreeze")]
        Antifreeze,

        [Display(Name = "Battery Liquid")]
        Battery_Liquid,

        [Display(Name = "Brake Liquid")]
        Brake_Liquid,


        //Car_Cosmetics subcategory
        [Display(Name = "Enamel")]
        Enamel,

        [Display(Name = "Varnish")]
        Varnish,

        [Display(Name = "Paint")]
        Paint,

        [Display(Name = "Body Cosmetics")]
        Body_Cosmetics,

        [Display(Name = "Salon Cosmetics")]
        Salon_Cosmetics,
    }
}
