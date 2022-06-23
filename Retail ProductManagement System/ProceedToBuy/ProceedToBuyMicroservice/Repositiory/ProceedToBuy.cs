using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProceedToBuyMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProceedToBuyMicroservice.Repositiory
{
    public class ProceedToBuy : IProceedToBuy
    {
        readonly string Baseurl = "http://localhost:50759/";

       
        public async Task<Cart> AddProductToCart(int CustomerId, int ProductId, int ZipCode)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Vendor/GetVendorDetails/" + ProductId);
                if (response.IsSuccessStatusCode)
                {
                    var Value = response.Content.ReadAsStringAsync().Result;
                    List<Vendor> obj = JsonConvert.DeserializeObject<List<Vendor>>(Value);
                    int rating = 0;
                    Vendor taggedVendor = null;
                    foreach (Vendor ven in obj)
                    {
                        if (ven.Rating > rating)
                        {
                            rating = ven.Rating;
                            taggedVendor = ven;
                        }
                    }
                    Random uniqueid = new Random();
                    Cart newCart = new Cart()
                    {
                        CartId = uniqueid.Next(1, 99999),
                        CustomerID = CustomerId,
                        ProductId = ProductId,
                        Zipcode = ZipCode,
                        DeliveryDate = DateTime.Now.AddDays(taggedVendor.ExpectedDateOfDelivery).ToString("yyyy-MM-dd"),
                        Vendorobject = taggedVendor
                    };
                    return newCart;
                }
                return null;
            }
        }

        
        public async Task<VendorWishlist> AddProductToWishlist(int CustomerId, int ProductId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Vendor/GetAllVendorDetails/" + ProductId);
                if (response.IsSuccessStatusCode)
                {
                    var Value = response.Content.ReadAsStringAsync().Result;
                    List<Vendor> obj = JsonConvert.DeserializeObject<List<Vendor>>(Value);
                    int rating = 0;
                    Vendor taggedVendor = null;
                    foreach (Vendor ven in obj)
                    {
                        if (ven.Rating > rating)
                        {
                            rating = ven.Rating;
                            taggedVendor = ven;
                        }
                    }
                    VendorWishlist wishlist = new VendorWishlist()
                    {
                        VendorId = taggedVendor.VendorId,
                        CustomerID = CustomerId,
                        ProductdId = ProductId,
                        Quanitity = 1,
                        DateAddedtoWishlist = DateTime.Now.ToString("yyyy-MM-dd")
                    };
                    return wishlist;
                }
                return null;
            }
        }
    }
}