using Web.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Model;
using Amazon.Runtime.Internal;


namespace Web.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDL _userDL;

        public UserController(IUserDL userDL)
        {
            _userDL = userDL;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserInsertRequest request)
        {
            Response response = new Response();
            try
            {
                response = await _userDL.CreateUser(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }
    }
}
