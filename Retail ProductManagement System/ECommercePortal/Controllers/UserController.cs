using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ECommercePortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommercePortal.Controllers
{
    public class UserController : Controller
    {
        Uri baseaddress = new Uri("http://localhost:50784/api");
        HttpClient client;
        public UserController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Auth(User user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                response = client.PostAsync(client.BaseAddress + "/Auth/Login", content).Result;
            }
            catch
            {
                return RedirectToAction("Error");
            }
            if (response.IsSuccessStatusCode)
            {
                string token = response.Content.ReadAsStringAsync().Result;

                if (token != null)
                {
                    TokenInfo.StringToken = token;
                    TokenInfo.UserID = user.UserID;
                    User user1 = new User();
                    HttpResponseMessage response1 = client.GetAsync(client.BaseAddress + "/Auth/GetUserDetails?id=" + user.UserID).Result;
                    string result = response1.Content.ReadAsStringAsync().Result;
                    user1 = JsonConvert.DeserializeObject<User>(result);
                    TokenInfo.username = user1.username;
                    return RedirectToAction("Index", "Customer");
                    //TokenInfo.username = user.username;
                   // return RedirectToAction("Detail", "User",user.UserID);
                    //return RedirectToAction("Index", "Customer");
                }
            }
            TokenInfo.StringToken = "";
            TokenInfo.UserID = 0;
            TokenInfo.username = "";

            return RedirectToAction("Login");

        }
        public IActionResult Logout()
        {
            TokenInfo.StringToken = null;
            TokenInfo.UserID = 0;
            TokenInfo.username = null;

            return RedirectToAction("Login");
        }
        public ActionResult Error()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Detail(int id)
        {
            User user = new User();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Auth/GetUserDetails?id=" + id).Result;
            string result = response.Content.ReadAsStringAsync().Result;
            user = JsonConvert.DeserializeObject<User>(result);
            //TokenInfo.username = user.username;
            return RedirectToAction("Index", "Customer");
        }
    }
}
