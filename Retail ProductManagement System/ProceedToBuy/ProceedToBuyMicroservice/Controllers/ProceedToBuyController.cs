using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProceedToBuyMicroservice.Models;
using ProceedToBuyMicroservice.Repositiory;

namespace ProceedToBuyMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    //[Authorize]
    public class ProceedToBuyController : ControllerBase
    {

        IProceedToBuy ptbrepo;
        readonly log4net.ILog _log4net;
        public ProceedToBuyController(IProceedToBuy _ptbrepo)
        {
            ptbrepo = _ptbrepo;
            _log4net = log4net.LogManager.GetLogger(typeof(ProceedToBuyController));
        }
        [HttpPost]
        [Route("AddProductToCart/{CustomerId}/{ProductId}/{ZipCode}")]
        public async Task<IActionResult> AddProductToCart(int CustomerId, int ProductId, int ZipCode)
        {
            Cart details = await ptbrepo.AddProductToCart(CustomerId, ProductId, ZipCode);
            try
            {
                _log4net.Info("ProceedToBuyController - AddProductToCart");
                if (details == null)
                {
                    _log4net.InfoFormat("ProceedToBuyController - AddProductToCart - Http POST Request for AddProductToCart Failed for Product Id: {0} by Customer Id: {1}", ProductId, CustomerId);
                    return NotFound();
                }
                else
                {
                    _log4net.InfoFormat("ProceedToBuyController - AddProductToCart - Http POST Request for AddProductToCart Completed for Product Id: {0} by Customer Id: {1}", ProductId, CustomerId);
                    return Ok(details);
                }
            }
            catch (Exception)
            {
                _log4net.InfoFormat("ProceedToBuyController - AddProductToCart - BadRequest");
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("AddProductToWishlist/{CustomerId}/{ProductId}")]
        public async Task<IActionResult> AddProductToWishlist(int CustomerId, int ProductId)
        {
            VendorWishlist details = await ptbrepo.AddProductToWishlist(CustomerId, ProductId);
            try
            {
                _log4net.Info("ProceedToBuyController - AddProductToWishlist");
                if (details == null)
                {
                    _log4net.InfoFormat("ProceedToBuyController - AddProductToWishlist - Http POST Request for AddProductToWishlist Failed for Product Id: {0} by Customer Id: {1}", ProductId, CustomerId);
                    return NotFound();
                }
                else
                {
                    _log4net.InfoFormat("ProceedToBuyController - AddProductToWishlist - Http POST Request for AddProductToWishlist Completed for Product Id: {0} by Customer Id: {1}", ProductId, CustomerId);
                    return Ok(details);
                }
            }
            catch (Exception)
            {
                _log4net.InfoFormat("ProceedToBuyController - AddProductToWishlist - BadRequest");
                return BadRequest();
            }
        }
    }
}
