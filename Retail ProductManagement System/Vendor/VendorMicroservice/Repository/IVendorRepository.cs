using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorMicroservice.Models;

namespace VendorMicroservice.Repository
{
    public interface IVendorRepository
    {
        public List<Vendor> GetVendorDetails(int ProductId);
        public List<Vendor> GetAllVendorDetails(int ProductId);
    }
}
