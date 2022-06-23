using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommercePortal.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ECommercePortal.Controllers
{
    public class CustomerController : Controller
    {
        EcommerceDbContext context;
        string token = TokenInfo.StringToken;
        public CustomerController(EcommerceDbContext _context)
        {
            context = _context;

        }
        // GET: CustomerController
        public async Task<IActionResult> Index()
        {
           
            if (token == null)
            {
               return RedirectToAction("Login","User");
            }
            var productdata = new List<Product>();

            using (var httpclient = new HttpClient())
            { 
                 
                //  httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                httpclient.BaseAddress = new Uri("http://localhost:54183/");
                HttpResponseMessage response = await httpclient.GetAsync(httpclient.BaseAddress + "api/Product/GetAllProducts");
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    productdata = JsonConvert.DeserializeObject<List<Product>>(result);
                    ViewBag.username = TokenInfo.username;
                    //for (int i = 0; i < productdata.Count; i++)
                    //{
                    //    context.products.Add(productdata[i]);
                    //    context.SaveChanges();
                    //}
                  
                  
                    return View(productdata);
                }
                else
                {
                    return BadRequest();
                }

            }
        }
         // GET: CustomerController/Create
        public IActionResult SearchById()
        {
            if (token == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchById(Product product)
        {
            var productdata = new Product();
            using (var httpclient = new HttpClient())
            { 
                httpclient.BaseAddress = new Uri("http://localhost:54183/");
                HttpResponseMessage response = await httpclient.GetAsync(httpclient.BaseAddress+"api/Product/SearchProductById?productId=" + product.ProductId);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    productdata = JsonConvert.DeserializeObject<Product>(result);
                    return RedirectToAction("Status", "Customer", productdata);
                }
                else
                {
                    return View("ProductNotAvailable");
                }
               
            }
            
        }
     
        public IActionResult Status(Product productdata)
        {
            return View(productdata);
        }

        public IActionResult SearchByName()
        {
            if (token == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> SearchByName(Product product)
        {
            var productdata = new Product();
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:54183/");
                HttpResponseMessage response = await httpclient.GetAsync(httpclient.BaseAddress + "api/Product/SearchProductByName?productName="+ product.ProductName);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    productdata = JsonConvert.DeserializeObject<Product>(result);
                    return RedirectToAction("Status", "Customer", productdata);
                }
                else
                {
                    return View("ProductNotAvailable1");
                }

            }

        }
        public async Task<IActionResult> Edit(int productid)
        {
            if (token == null)
            {
                return RedirectToAction("Login", "User");
            }
            var productdata = new Product();
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:54183/");
                HttpResponseMessage response = await httpclient.GetAsync(httpclient.BaseAddress + "api/Product/SearchProductById?productId=" + productid);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    productdata = JsonConvert.DeserializeObject<Product>(result);
                    return View(productdata);
                }
                else
                {
                    return View("ProductNotAvailable");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:54183/");
                string data = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpclient.PutAsync(httpclient.BaseAddress + "api/Product/AddProductRating/" + product.ProductId + "/" + product.Rating, content);
                if (response.IsSuccessStatusCode)
                {
                    UpdatedRating rate = new UpdatedRating();
                    rate.userid = TokenInfo.UserID;
                    rate.ProductId = product.ProductId;
                    rate.Rating = product.Rating;
                    context.products.Add(rate);
                    context.SaveChanges();
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    return View("Invalid");
                }
            }
        } 

       
      
        public IActionResult AddProductToCart()
        {
            if (token == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToCart(Show cart)
        {
            cart.CustomerID = TokenInfo.UserID;
            using (var httpclient=new HttpClient()) {
                httpclient.BaseAddress = new Uri("http://localhost:53694/");
                string data = JsonConvert.SerializeObject(cart);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpclient.PostAsync(httpclient.BaseAddress + "api/ProceedToBuy/AddProductToCart/" + cart.CustomerID + "/" + cart.ProductId + "/" + cart.Zipcode,content);
              
                if (response.IsSuccessStatusCode)
                {
                    //string data1 = response.Content.ReadAsStringAsync().Result;
                    //Show cartvalue = JsonConvert.DeserializeObject<Show>(data1);
                    //context.cart.Add(cartvalue);
                    //context.SaveChanges();
                    //return RedirectToAction("CartStatus", "Customer", cartvalue);
                    string data1 = response.Content.ReadAsStringAsync().Result;
                    Cart value = JsonConvert.DeserializeObject<Cart>(data1);
                    Show cartvalue = new Show()
                    {
                        CartId = value.CartId,
                        CustomerID = value.CustomerID,
                        ProductId = value.ProductId,
                        Zipcode = value.Zipcode,
                        DeliveryDate = value.DeliveryDate,
                        VendorId = value.Vendorobject.VendorId,
                        VendorName = value.Vendorobject.VendorName,
                        DeliveryCharge = value.Vendorobject.DeliveryCharge,
                        Rating = value.Vendorobject.Rating,
                        ExpectedDateOfDelivery = value.Vendorobject.ExpectedDateOfDelivery
                    };

                    context.cart.Add(cartvalue);
                    context.SaveChanges();
                    return RedirectToAction("CartStatus", "Customer", cartvalue);

                }
                else
                {
                  //  return View("ProductNotInStock");
                   return RedirectToAction("AddProductToWishlist","Customer", cart);

                }
            }          
        }
        public IActionResult AddProductToCart1(int productid)
        {
            if (token == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToCart1(Show cart)
        {
            cart.CustomerID = TokenInfo.UserID;
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:53694/");
                string data = JsonConvert.SerializeObject(cart);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpclient.PostAsync(httpclient.BaseAddress + "api/ProceedToBuy/AddProductToCart/" + cart.CustomerID + "/" + cart.ProductId + "/" + cart.Zipcode, content);

                if (response.IsSuccessStatusCode)
                {
                    string data1 = response.Content.ReadAsStringAsync().Result;
                    Cart value = JsonConvert.DeserializeObject<Cart>(data1);
                    Show cartvalue = new Show()
                    {
                        CartId = value.CartId,
                        CustomerID = value.CustomerID,
                        ProductId = value.ProductId,
                        Zipcode = value.Zipcode,
                        DeliveryDate = value.DeliveryDate,
                        VendorId = value.Vendorobject.VendorId,
                        VendorName = value.Vendorobject.VendorName,
                        DeliveryCharge = value.Vendorobject.DeliveryCharge,
                        Rating = value.Vendorobject.Rating,
                        ExpectedDateOfDelivery = value.Vendorobject.ExpectedDateOfDelivery
                    };

                    context.cart.Add(cartvalue);
                    context.SaveChanges();
                    return RedirectToAction("CartStatus", "Customer", cartvalue);

                }
                else
                {
                    //  return View("ProductNotInStock");
                    return RedirectToAction("AddProductToWishlist", "Customer", cart);
                }
            }
        }
       public IActionResult CartStatus(Show cart)
        {          
            return View(cart);
        }

        public async Task<IActionResult> AddProductToWishlist(Cart cart)
        {
            //http://localhost:53694/api/ProceedToBuy/AddProductToCart/1/1/221006
            //http://localhost:53694/api/ProceedToBuy/AddProductToWishlist/1/3
            using (var httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new Uri("http://localhost:53694/");
                string data = JsonConvert.SerializeObject(cart);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpclient.PostAsync(httpclient.BaseAddress + "api/ProceedToBuy/AddProductToWishlist/" + cart.CustomerID + "/" + cart.ProductId, content);
                if (response.IsSuccessStatusCode)
                {
                    return View("WishlistStatus", "Customer");
                }
                return View("ProductNotAvailable2");
            }
        }


     
        public IActionResult WishlistStatus()
        {
            return View();
        }



    }
}
