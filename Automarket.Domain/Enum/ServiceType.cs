using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Enum
{
    public enum ServiceType
    {
        [Description("Engine Diagnostics")]
        Engine_Service,

        [Description("Clutch Diagnostics")]
        Clutch_Service,

        [Description("Computer Diagnostics")]
        Computer_Diagnostics_Service,

        [Description("Chassis Diagnostics")]
        Chassis_Service,

        [Description("Turbine Diagnostics")]
        Turbine_Service,

        [Description("Wheel Alignment")]
        Wheel_Alignment_Service,

        [Description("Electricity Diagnostics")]
        Electricity_Service,

        [Description("Steering Rack")]
        Steering_Rack_Service,

        [Description("Auto Shop")]
        Car_Shop_Service,
    }
}
