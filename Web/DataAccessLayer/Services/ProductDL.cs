using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Model;

namespace Web.DataAccessLayer.Services
{
    public class ProductDL : IProductDL
    {
        private readonly IMongoCollection<Product> _products;

        public ProductDL(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["Database:ConnectionString"]);
            var database = client.GetDatabase(configuration["Database:DatabaseName"]);
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<Product> CreateProduct(Product product)
        {
            product.CreatedDate = DateTime.UtcNow;
            product.ModifiedDate = DateTime.UtcNow;
            await _products.InsertOneAsync(product);
            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<Product> GetProductById(string productId)
        {
            return await _products.Find(product => product.ProductID == productId).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            product.ModifiedDate = DateTime.UtcNow;
            var result = await _products.ReplaceOneAsync(p => p.ProductID == product.ProductID, product);
            return result.IsAcknowledged && result.ModifiedCount == 1;
        }

        public async Task<bool> DeleteProduct(string productId)
        {
            var result = await _products.DeleteOneAsync(p => p.ProductID == productId);
            return result.IsAcknowledged && result.DeletedCount == 1;
        }

        public async Task<List<Product>> GetProductsByVendor(string vendorId)
        {
            return await _products.Find(product => product.VendorID == vendorId).ToListAsync();
        }
    }
}
