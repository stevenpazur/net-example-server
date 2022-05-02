using ClassLibrary3.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ILogger<OrdersService> _logger;
        private readonly modelContext _mContext;

        public OrdersService(ILogger<OrdersService> logger, modelContext mContext)
        {
            _logger = logger;
            _mContext = mContext;
        }

        public IEnumerable<Orders> GetOrders()
        {
            try
            {
                return _mContext.Orders.Take(500);
            }
            catch(Exception ex)
            {
                _logger.LogError("OrdersService: error in GetOrders");
                throw new Exception(ex.Message, ex);
            }
        } 

        public async Task NewOrder(List<string> items)
        {
            try
            {
                var ItemsAsAString = ListToString(items, ';');
                var newOrder = new Orders { Items = ItemsAsAString };
                await _mContext.Orders.AddAsync(newOrder);
                _mContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError("OrdersService: error in NewOrder");
                throw new Exception(ex.Message, ex);
            }
        }

        private string ListToString(List<string> listItems, char delimiter)
        {
            var result = "";

            foreach(var item in listItems)
            {
                result += string.Format("{0}{1}", item, delimiter.ToString());
            }

            return result.Trim(delimiter);
        }
    }
}
