using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommercePortal.Models
{
    public class User
    {
        [Required]
        public int UserID { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public string username { get; set; }
    }
}
