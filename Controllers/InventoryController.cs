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
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _inventoryService;

        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public ActionResult<Inventory> GetAllInventory()
        {
            try
            {
                return Ok(_inventoryService.GetAllInventory());
            }
            catch (Exception ex)
            {
                _logger.LogError("InventoryController: error in GetAllInventory");
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("{shopId}")]
        public ActionResult<Inventory> GetInventoryByShopId(int shopId)
        {
            try
            {
                return Ok(_inventoryService.GetInventoryByShopId(shopId));
            }
            catch(Exception ex)
            {
                _logger.LogError("InventoryController: error in GetInventoryByShopId");
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPut]
        public ActionResult UpdateInventoryForShop([FromBody] Inventory inventory)
        {
            try
            {
                return Ok(_inventoryService.UpdateInventoryForShop(inventory));
            }
            catch(Exception ex)
            {
                _logger.LogError("InventoryController: error in UpdateInventoryForShop");
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
