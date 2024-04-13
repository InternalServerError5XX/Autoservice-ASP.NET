using Automarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.ViewModels.AdminPanel
{
    public class AdminPanelViewModel
    {
        public List<User> Users { get; set; }
        public List<Consumable> Items { get; set; }
        public List<Appointment> Appointments { get; set; }
    }

}
