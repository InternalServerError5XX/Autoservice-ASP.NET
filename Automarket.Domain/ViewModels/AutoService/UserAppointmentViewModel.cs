using Automarket.Domain.Entity;
using Automarket.Domain.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.ViewModels.AutoService
{
    public class UserAppointmentViewModel
    {
        public User User { get; set; }
        public AppointmentViewModel Appointment { get; set; }
    }
}
