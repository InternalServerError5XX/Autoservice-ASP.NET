using Automarket.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Entity
{
    public class Maintenance
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? AppointmentId { get; set; }
        public ServiceStage Stage { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }
        public DateTime CompletionTime { get; set; }
    }
}
