using Microsoft.AspNetCore.Mvc;
using Web.DataAccessLayer;
using Web.Model;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Authorize]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminDL _adminDL;
        private readonly IConfiguration _configuration;  

        public AdminController(IAdminDL adminDL, IConfiguration configuration) 
        {
            _adminDL = adminDL;
            _configuration = configuration; 
        }              
        
        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            return Ok(await _adminDL.GetAllAdmins());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdminById(string id)
        {
            var admin = await _adminDL.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdmin([FromBody] Admin admin)
        {
            if (await _adminDL.UpdateAdmin(admin))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            if (await _adminDL.DeleteAdmin(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
