using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommercePortal.Models
{
    public class UpdatedRating
    {
        public int id { get; set; }
        public int userid { get; set; }
        public int ProductId { get; set; }
        public double Rating { get; set; }
    }
}
