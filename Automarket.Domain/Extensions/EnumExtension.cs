using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this System.Enum value)
        {
            return value.GetType()
                        .GetMember(value.ToString())
                        .FirstOrDefault()
                        ?.GetCustomAttribute<DisplayAttribute>()
                        ?.Name
                        ?? value.ToString();
        }
    }
}
