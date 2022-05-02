using ClassLibrary3.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly ILogger<ShopController> _logger;
        private readonly IShopService _shopService;

        public ShopController(ILogger<ShopController> logger, IShopService shopService)
        {
            _logger = logger;
            _shopService = shopService;
        }

        /// <summary>
        ///     Returns a list of all stores in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try
            {
                return Ok(_shopService.GetShops());
            }
            catch(Exception ex)
            {
                _logger.LogError("ShopController: error in Get");
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        ///     Adds a new store to the database
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] Shop shop)
        {
            try
            {
                
                return Ok(_shopService.PostShop(shop));
            }
            catch(Exception ex)
            {
                _logger.LogError("ShopController: error in Post");
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPut]
        public ActionResult UpdateShop([FromBody] Shop shop)
        {
            try
            {
                return Ok(_shopService.UpdateShop(shop));
            }
            catch(Exception ex)
            {
                _logger.LogError("ShopController: error in UpdateShop");
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
