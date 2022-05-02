using ClassLibrary3.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class ShopService : IShopService
    {
        private readonly ILogger<ShopService> _logger;
        private readonly modelContext _modelContext;

        public ShopService(ILogger<ShopService> logger, modelContext mContext)
        {
            _logger = logger;
            _modelContext = mContext;
        }

        public IEnumerable<Shop> GetShops()
        {
            try
            {
                return _modelContext.Shops.ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError("ShopService: error in GetShops");
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task PostShop(Shop shop)
        {
            try
            {
                await _modelContext.Shops.AddAsync(shop);
                _modelContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError("ShopService: error in PostShop");
                throw new Exception(ex.Message, ex);
            }
        }

        public Task UpdateShop(Shop shop)
        {
            try
            {
                _modelContext.Shops.Update(shop);
                _modelContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError("ShopService: error in UpdateShop");
                throw new Exception(ex.Message, ex);
            }

            return null;
        }
    }
}
