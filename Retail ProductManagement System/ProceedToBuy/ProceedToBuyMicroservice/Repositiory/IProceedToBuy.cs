using ProceedToBuyMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyMicroservice.Repositiory
{
    public interface IProceedToBuy
    {
        //public Cart AddProductToCart(List<Vendor> Vendorlist);
        public Task<Cart> AddProductToCart(int CustomerId, int ProductId, int ZipCode);
        public Task<VendorWishlist> AddProductToWishlist(int CustomerId, int ProductId);
    }
}
