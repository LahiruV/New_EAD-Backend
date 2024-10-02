using Web.Model;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.DataAccessLayer.Services
{
    public class AdminDL : IAdminDL
    {
        private readonly IMongoCollection<Admin> _admins;

        public async Task<Admin> GetAdminByUsername(string username)
        {
            return await _admins.Find(admin => admin.Username == username).FirstOrDefaultAsync();
        }
        public AdminDL(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["Database:ConnectionString"]);
            var database = client.GetDatabase(configuration["Database:DatabaseName"]);
            _admins = database.GetCollection<Admin>("Admins");
        }

        public async Task<Admin> CreateAdmin(Admin admin)
        {
            admin.SetPassword(admin.Password);
            await _admins.InsertOneAsync(admin);
            return admin;
        }

        public async Task<List<Admin>> GetAllAdmins()
        {
            return await _admins.Find(_ => true).ToListAsync();
        }

        public async Task<Admin> GetAdminById(string id)
        {
            return await _admins.Find(admin => admin.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAdmin(Admin admin)
        {
            var existingAdmin = await GetAdminById(admin.Id);
            if (existingAdmin != null)
            {
                admin.Password = !string.IsNullOrEmpty(admin.Password) ? BCrypt.Net.BCrypt.HashPassword(admin.Password) : existingAdmin.Password;
            }
            var result = await _admins.ReplaceOneAsync(a => a.Id == admin.Id, admin);
            return result.IsAcknowledged && result.ModifiedCount == 1;
        }

        public async Task<bool> DeleteAdmin(string id)
        {
            var result = await _admins.DeleteOneAsync(a => a.Id == id);
            return result.IsAcknowledged && result.DeletedCount == 1;
        }
    }
}
