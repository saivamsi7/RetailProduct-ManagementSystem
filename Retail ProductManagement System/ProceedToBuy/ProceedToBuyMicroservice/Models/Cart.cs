using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyMicroservice.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int CustomerID { get; set; }
        public int ProductId { get; set; }
        public int Zipcode { get; set; }
        public string DeliveryDate { get; set; }
        public Vendor Vendorobject { get; set; }
    }
}
