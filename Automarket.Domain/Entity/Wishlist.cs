using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Domain.Entity
{
    public class Wishlist
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ItemId { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
    }
}
