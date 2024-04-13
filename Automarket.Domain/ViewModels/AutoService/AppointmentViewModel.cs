using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.ViewModels.Order
{
    public class AppointmentViewModel
    {
        public long Id { get; set; }
        public long? UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage = "Name must be greater than 1")]
        [MaxLength(30, ErrorMessage = "Name must be less than 30")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [MinLength(18, ErrorMessage = "Phone number incorect")]
        [MaxLength(20, ErrorMessage = "Phone number must be less than 20")]
        public string PhoneNumber { get; set; }

        public bool CallBack { get; set; } = false;

        [MaxLength(500, ErrorMessage = "Description must be less than 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Appointment date is required")]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreationDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EditDate { get; set; }

        public bool Checked { get; set; } = false;
    }
}
