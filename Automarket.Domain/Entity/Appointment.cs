using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Entity
{
    public class Appointment
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool CallBack { get; set; } = false;
        public string? Description { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
        public bool Checked { get; set; } = false;
    }
}
