using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommercePortal.Models
{
    public class Cart
    { public int id { get; set; }
        public int CartId { get; set; }
        public int CustomerID { get; set; }
        public int ProductId { get; set; }
        public int Zipcode { get; set; }
        public string DeliveryDate { get; set; }
        public Vendor Vendorobject { get; set; }
    }
}
