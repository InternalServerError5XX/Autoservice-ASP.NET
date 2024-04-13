using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.ViewModels.Account
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [MinLength(7, ErrorMessage = "Email must be greater than 7")]
        [MaxLength(50, ErrorMessage = "Email must be less than 50")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be greater than 6")]
        [MaxLength(30, ErrorMessage = "Password must be less than 30")]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Repeat your password")]
        //[DataType(DataType.Password)]
        //[MinLength(6, ErrorMessage = "Password must be greater than 6")]
        //[MaxLength(30, ErrorMessage = "Password must be less than 30")]
        //[Compare("Password", ErrorMessage = "Passwords do not match")]
        //public string RepeatPassword { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name must be greater than 1")]
        [MaxLength(30, ErrorMessage = "Name must be less than 30")]
        public string Name { get; set; }
    }
}
