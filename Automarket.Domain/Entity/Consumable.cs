using Automarket.Domain.Enum;
using Automarket.Domain.Enum.ConsumableType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Entity
{
    public class Consumable
    {
        public long Id { get; set; }
        public ConsumableCategory ConsumableType { get; set; }
        public ConsumableSubcategory Subcategory { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string? Country { get; set; }
        public int? Year { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }       
        public string PhotoPath { get; set; }
    }
}
