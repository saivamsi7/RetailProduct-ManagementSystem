using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommercePortal.Models
{
    public class Product
    {  
        public int id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
    }
}
