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
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrdersService _ordersService;

        public OrdersController(ILogger<OrdersController> logger, IOrdersService ordersService)
        {
            _logger = logger;
            _ordersService = ordersService;
        }

        /// <summary>
        ///     Gets the first 500 orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<Orders> GetOrders()
        {
            try
            {
                return Ok(_ordersService.GetOrders());
            }
            catch(Exception ex)
            {
                _logger.LogError("OrdersController: error in GetOrders");
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPost]
        public ActionResult AddOrder(List<string> items)
        {
            try
            {
                return Ok(_ordersService.NewOrder(items));
            }
            catch(Exception ex)
            {
                _logger.LogError("OrdersController: error in AddOrder");
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
