using Automarket.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Entity
{
    public class Car
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public int Speed { get; set; }

        public DateTime Date { get; set; }

        public decimal Price { get; set; }

        public CarType CarType { get; set; }
    }
}
