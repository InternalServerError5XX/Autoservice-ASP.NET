using System.ComponentModel.DataAnnotations;
using Automarket.Domain.Enum;
using Automarket.Domain.Helpers;

namespace Automarket.Domain.ViewModels.Account
{
    public class AccountViewModel
    {
        public long Id { get; set; }

        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MinLength(7, ErrorMessage = "Email must be greater than 7")]
        [MaxLength(50, ErrorMessage = "Email must be less than 50")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name must be greater than 1")]
        [MaxLength(30, ErrorMessage = "Name must be less than 30")]
        public string Name { get; set; }

        [MinLength(2, ErrorMessage = "Name must be greater than 1")]
        [MaxLength(30, ErrorMessage = "Name must be less than 30")]
        public string? Lastname { get; set; }

        [ValidationHelper(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be greater than 6")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Range(12, 120, ErrorMessage = "Age must be between 12 and 120.")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        [EnumDataType(typeof(Role), ErrorMessage = "Invalid role.")]
        public Role Role { get; set; }
    }
}
