﻿using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Model;

namespace Web.DataAccessLayer.Services
{
    public class InventoryDL : IInventoryDL
    {
        private readonly IMongoCollection<Inventory> _inventories;

        public InventoryDL(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["Database:ConnectionString"]);
            var database = client.GetDatabase(configuration["Database:DatabaseName"]);
            _inventories = database.GetCollection<Inventory>("Inventories");
        }

        public async Task<Inventory> CreateInventory(Inventory inventory)
        {
            await _inventories.InsertOneAsync(inventory);
            return inventory;
        }

        public async Task<Inventory> GetInventoryByProductId(string productId)
        {
            return await _inventories.Find(inv => inv.ProductID == productId).FirstOrDefaultAsync();
        }

        public async Task<List<Inventory>> GetAllInventories()
        {
            return await _inventories.Find(_ => true).ToListAsync();
        }

        public async Task<bool> UpdateInventory(Inventory inventory)
        {
            var updateResult = await _inventories.ReplaceOneAsync(inv => inv.InventoryId == inventory.InventoryId, inventory);
            if (updateResult.ModifiedCount == 1)
            {
                // Check and update the stock alert status
                inventory.StockAlert = inventory.StockLevel <= inventory.LowStockThreshold;
                await _inventories.ReplaceOneAsync(inv => inv.InventoryId == inventory.InventoryId, inventory);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteInventory(string inventoryId)
        {
            var deleteResult = await _inventories.DeleteOneAsync(inv => inv.InventoryId == inventoryId);
            return deleteResult.DeletedCount == 1;
        }
    }
}
