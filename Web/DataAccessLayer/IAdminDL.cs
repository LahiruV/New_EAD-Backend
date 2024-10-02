using Web.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.DataAccessLayer
{
    public interface IAdminDL
    {
        Task<Admin> CreateAdmin(Admin admin);
        Task<List<Admin>> GetAllAdmins();
        Task<Admin> GetAdminById(string id);
        Task<bool> UpdateAdmin(Admin admin);
        Task<bool> DeleteAdmin(string id);
        Task<Admin> GetAdminByUsername(string username);

    }
}
