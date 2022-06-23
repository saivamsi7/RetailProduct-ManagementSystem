using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyMicroservice.Models
{
    public class VendorWishlist
    {
        public int VendorId { get; set; }
        public int CustomerID { get; set; }
        public int ProductdId { get; set; }
        public int Quanitity { get; set; }
        public string DateAddedtoWishlist { get; set; }
    }
}
