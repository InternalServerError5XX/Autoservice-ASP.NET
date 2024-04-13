using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [MinLength(7, ErrorMessage = "Email must be greater than 7")]
        [MaxLength(50, ErrorMessage = "Email must be less than 50")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be greater than 6")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
