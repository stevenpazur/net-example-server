using ClassLibrary3.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ILogger<InventoryService> _logger;
        private readonly modelContext _modelContext;
        private readonly IEnumerable<Shop> Shops;

        public InventoryService(ILogger<InventoryService> logger, modelContext context)
        {
            _logger = logger;
            _modelContext = context;
            Shops = _modelContext.Shops.ToList();
        }

        public IEnumerable<Inventory> GetInventoryByShopId(int shopId)
        {
            try
            {
                return _modelContext.Inventories.Where(x => x.ShopId == shopId);
            }
            catch(Exception ex)
            {
                _logger.LogError("InventoryService: error in GetInventoryByShopId");
                throw new Exception(ex.Message, ex);
            }
        }

        public IEnumerable<Inventory> GetAllInventory()
        {
            try
            {
                return _modelContext.Inventories;
            }
            catch(Exception ex)
            {
                _logger.LogError("InventoryService: error in GetAlInventory");
                throw new Exception(ex.Message, ex);
            }
        }

        public Task UpdateInventoryForShop(Inventory inventory)
        {
            try
            {
                bool added = false;
                foreach(var shop in Shops)
                {
                    if(shop.ShopId == inventory.ShopId)
                    {
                        var i = _modelContext.Inventories.Where(x => x.ShopId == inventory.ShopId).First();
                        i.ChocolateBarrels += inventory.ChocolateBarrels;
                        i.VanillaBarrels += inventory.VanillaBarrels;
                        i.CookieJars += inventory.CookieJars;
                        i.StrawberryBarrels += inventory.StrawberryBarrels;
                        _modelContext.Update(i);
                        _modelContext.SaveChanges();
                        added = true;
                    }
                }
                if (!added)
                {
                    _logger.LogWarning("Shop Id does not exist.");
                    throw new Exception("UpdateInventoryForShop: Shop Id does not exist.");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("InventoryService: error in UpdateInventoryForShop");
                throw new Exception(ex.Message, ex);
            }

            return null;
        }
    }
}
