using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Model;

namespace Web.DataAccessLayer.Services
{
    public class OrderDL : IOrderDL
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderDL(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["Database:ConnectionString"]);
            var database = client.GetDatabase(configuration["Database:DatabaseName"]);
            _orders = database.GetCollection<Order>("Orders");
        }

        public async Task<Order> CreateOrder(Order order)
        {            
            await _orders.InsertOneAsync(order);
            return order;
        }

        public async Task<Order> GetOrderById(string orderId)
        {
            return await _orders.Find(order => order.OrderID == orderId).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _orders.Find(_ => true).ToListAsync();
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            var updateResult = await _orders.ReplaceOneAsync(o => o.OrderID == order.OrderID, order);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount == 1;
        }

        public async Task<bool> CancelOrder(string orderId, string cancellationReason)
        {
            var update = Builders<Order>.Update.Set(o => o.OrderStatus, "Cancelled").Set(o => o.CancellationReason, cancellationReason);
            var result = await _orders.UpdateOneAsync(o => o.OrderID == orderId, update);
            return result.IsAcknowledged && result.ModifiedCount == 1;
        }
    }
}
