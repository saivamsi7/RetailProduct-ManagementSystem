using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductMicroservice.Repositories;

namespace ProductMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ProductController : ControllerBase
    {
        IProductRepository prodrepo;

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ProductController));

        public ProductController(IProductRepository _prodrepo)
        {
            prodrepo = _prodrepo;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            _log4net.Info("ProductController - GetAllProducts");

            try
            {
                var result = prodrepo.GetAllProducts();

                if (result != null)
                {
                    _log4net.Info("ProductController - GetAllProducts - All Products Fetched");

                    return Ok(result);
                }
                _log4net.Info("ProductController - GetAllProducts - Products Not Found");
                return NotFound();
            }

            catch (Exception)
            {
                _log4net.Info("ProductController - GetAllProducts - Bad Request");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("SearchProductById")]
        public IActionResult SearchProductById(int productId)
        {
            _log4net.Info("ProductController - SearchProductById");

            try
            {
                var result = prodrepo.SearchProductById(productId);

                if (result != null)
                {
                    _log4net.Info("ProductController - SearchProductById - Successfuly Searched Product Id: " + productId);

                    return Ok(result);
                }
                _log4net.Info("ProductController - SearchProductById - Search Failed for Product Id: " + productId);
                return NotFound();
            }

            catch (Exception)
            {
                _log4net.Info("ProductController - SearchProductById - Bad Request");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("SearchProductByName")]
        public IActionResult SearchProductByName(string productName)
        {
            _log4net.Info("ProductController - SearchProductByName");

            try
            {
                var result = prodrepo.SearchProductByName(productName);

                if (result != null)
                {
                    _log4net.Info("ProductController - SearchProductByName - Successfuly Searched Product Name: " + productName);

                    return Ok(result);
                }
                _log4net.Info("ProductController - SearchProductByName - Search Failed for Product Name: " + productName);
                return NotFound();
            }

            catch (Exception)
            {
                _log4net.Info("ProductController - SearchProductByName - Bad Request");
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("AddProductRating/{productId}/{rating}")]
        public IActionResult AddProductRating(int productId, double rating)
        {
            _log4net.Info("ProductController - AddProductRating");

            double[] validRating = new double[5] {1,2,3,4,5};

            if (!validRating.Contains(rating))
            {
                return BadRequest("Enter a valid rating");
            }

            try
            {
                int result = prodrepo.AddProductRating(productId, rating);

                if (result == 1)
                {
                    _log4net.Info("ProductController - AddProductRating - Successfully Added Rating for Product Id: " + productId);

                    return Ok();
                }
                return NotFound();
            }

            catch (Exception)
            {
                _log4net.Info("ProductController - AddProductRating - Bad Request");
                return BadRequest();
            }
        }
    }
}
