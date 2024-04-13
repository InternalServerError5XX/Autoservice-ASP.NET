using Automarket.Domain.Enum;
using System;
using System.ComponentModel;
using System.Reflection;

namespace Automarket.Domain.Extensions
{
    public static class ServiceTypeExtensions
    {
        public static string GetDisplayName(this ServiceType value)
        {
            var field = value.GetType().GetField(value.ToString());

            if (field == null)
            {
                value.ToString();
            }
                
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            return attribute?.Description ?? value.ToString();
        }
    }
}
