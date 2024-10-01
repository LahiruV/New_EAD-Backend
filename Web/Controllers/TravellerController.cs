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
    public class TravellerController : ControllerBase
    {
        private readonly ITravellerDL _travellerDL;

        public TravellerController(ITravellerDL travellerDL)
        {
            _travellerDL = travellerDL;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TravellerInsertRequest request)
        {
            Response response = new Response();
            try
            {
                response = await _travellerDL.Create(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            GetAllTravellerResponse response = new GetAllTravellerResponse();
            try
            {
                response = await _travellerDL.GetAll();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByNIC([FromQuery] string nic)
        {
            GetTravellerByNICResponse response = new GetTravellerByNICResponse();
            try
            {
                response = await _travellerDL.GetByNIC(nic);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateByNIC(TravellerInsertRequest request)
        {
            Response response = new Response();
            try
            {
                response = await _travellerDL.UpdateByNIC(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteTravellerRequest request)
        {
            Response response = new Response();
            try
            {
                response = await _travellerDL.Delete(request);
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
