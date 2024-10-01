using Web.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Model;
using Amazon.Runtime.Internal;
using Web.DataAccessLayer.Services;


namespace Web.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserDL _userDL;
        private readonly ITravellerDL _travellerDL;

        public LoginController(IUserDL userDL, ITravellerDL travellerDL)
        {
            _userDL = userDL;
            _travellerDL = travellerDL;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(Login request)
        {

            GetAllUserResponse response = await _userDL.GetAll();
            var loggeduser = response.data.Where(x => x.Mail == request.Mail && x.Password == request.Password);

            if(loggeduser.Any())
            {
                return Ok(loggeduser);
            }
            else
            {
                return BadRequest("User Not Found");
            }   

        }

        [HttpPost]
        public async Task<IActionResult> TravellerLogin(Login request)
        {

            GetAllTravellerResponse response = await _travellerDL.GetAll();
            var loggeduser = response.data.Where(x => x.Mail == request.Mail && x.Password == request.Password);

            if (loggeduser.Any())
            {
                return Ok(loggeduser);
            }
            else
            {
                return BadRequest("User Not Found");
            }

        }
    }
}

