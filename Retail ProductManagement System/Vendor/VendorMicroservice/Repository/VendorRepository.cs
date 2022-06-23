using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendorMicroservice.Helper;
using VendorMicroservice.Models;

namespace VendorMicroservice.Repository
{
    public class VendorRepository : IVendorRepository
    {
       
        public List<Vendor> GetVendorDetails(int ProductId)
        {
            var venid = from s in Data.stocks where s.StockInHand >= 1 && s.ProductId == ProductId select s.VendorId;
            var venidlist = venid.ToList();
            if (venidlist.Count != 0)
            {
                List<Vendor> vendordetails = new List<Vendor>();
                for (int i = 0; i < venidlist.Count; i++)
                {
                    var vendor = Data.vendors.Where(x => x.VendorId == venidlist[i]).FirstOrDefault();
                    vendordetails.Add(vendor);
                }
                if (vendordetails.Count != 0)
                {
                    return vendordetails;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public List<Vendor> GetAllVendorDetails(int ProductId)
        {
            var venid = from s in Data.stocks where s.ProductId == ProductId select s.VendorId;
            var venidlist = venid.ToList();
            if (venidlist.Count != 0)
            {
                List<Vendor> vendordetails = new List<Vendor>();
                for (int i = 0; i < venidlist.Count; i++)
                {
                    var vendor = Data.vendors.Where(x => x.VendorId == venidlist[i]).FirstOrDefault();
                    vendordetails.Add(vendor);
                }
                return vendordetails;
            }
            return null;
        }
    }
}
