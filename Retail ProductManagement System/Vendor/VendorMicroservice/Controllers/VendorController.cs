using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendorMicroservice.Repository;

namespace VendorMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class VendorController : ControllerBase
    {
        IVendorRepository venrepo;
        readonly log4net.ILog _log4net;
        public VendorController(IVendorRepository _venrepo)
        {
            venrepo = _venrepo;
            _log4net = log4net.LogManager.GetLogger(typeof(VendorController));
        }
        
        [HttpGet]
        [Route("GetVendorDetails/{ProductId}")]
        public IActionResult GetVendorDetails(int ProductId)
        {
            _log4net.Info("VendorController - GetVendorDetails");

            try
            {
                _log4net.Info("VendorController - GetVendorDetails - Http GET Request for GetVendorDetails Started for Product Id: "+ProductId);

                var result = venrepo.GetVendorDetails(ProductId);
                
                if (result == null)
                {
                    _log4net.Info("VendorController - GetVendorDetails - Http GET Request for GetVendorDetails Failed for Product Id: " + ProductId);
                    return NotFound();
                }
                _log4net.Info("VendorController - GetVendorDetails - Http GET Request for GetVendorDetails Completed for Product Id: " + ProductId);
                
                return Ok(result);
            }
            catch (Exception)
            {
                _log4net.Info("VendorController - GetVendorDetails - BadRequest");
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetAllVendorDetails/{ProductId}")]
        public IActionResult GetAllVendorDetails(int ProductId)
        {
            _log4net.Info("VendorController - GetAllVendorDetails");

            try
            {
                _log4net.Info("VendorController - GetAllVendorDetails - Http GET Request for GetAllVendorDetails Started for Product Id: " + ProductId);
                var result = venrepo.GetAllVendorDetails(ProductId);

                if (result == null)
                {
                    _log4net.Info("VendorController - GetAllVendorDetails - Http GET Request for GetAllVendorDetails Failed for Product Id: " + ProductId);
                    return NotFound();
                }
                _log4net.Info("VendorController - GetAllVendorDetails - Http GET Request for GetAllVendorDetails Completed for Product Id: " + ProductId);
                return Ok(result);
            }
            catch (Exception)
            {
                _log4net.Info("VendorController - GetAllVendorDetails - BadRequest");
                return BadRequest();
            }
        }
    }
}
