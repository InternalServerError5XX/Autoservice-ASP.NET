using Automarket.Domain.Enum;
using Automarket.Domain.Enum.ConsumableType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.ViewModels.AutoService
{
    public class MaintenanceViewModel
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? AppointmentId { get; set; }

        [Required(ErrorMessage = "Service stage is required")]
        [EnumDataType(typeof(ServiceStage), ErrorMessage = "Invalid stage.")]
        public ServiceStage Stage { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }

        [Required(ErrorMessage = "Completion date is required")]
        [DataType(DataType.Date)]
        public DateTime CompletionTime { get; set; }
    }
}
