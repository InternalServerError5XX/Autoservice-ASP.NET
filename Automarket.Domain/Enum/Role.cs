using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Enum
{
    public enum Role
    {
        [Display(Name = "Admin")]
        Admin = 0,

        [Display(Name = "Administrator")]
        Administrator = 1,

        [Display(Name = "Mechanic")]
        Mechanic = 2,

        [Display(Name = "User")]
        User = 3,
    }
}
