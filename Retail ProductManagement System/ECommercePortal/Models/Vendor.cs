using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommercePortal.Models
{
    public class Vendor
    {
        public int id { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public double DeliveryCharge { get; set; }
        public int Rating { get; set; }
        public int ExpectedDateOfDelivery { get; set; }
    }
}
