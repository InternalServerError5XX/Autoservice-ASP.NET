using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Enum
{
    public enum CarType
    {
        [Display(Name = "Coupe")]
        Coupe = 0,

        [Display(Name = "Sedan")]
        Sedan = 1,

        [Display(Name = "Crossover")]
        Crossover = 2,

        [Display(Name = "Sport")]
        Sport = 3
    }
}
