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
    public class TrainController : ControllerBase
    {
        private readonly ITrainDL _trainDL;

        public TrainController(ITrainDL trainDL)
        {
            _trainDL = trainDL;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrainInsertRequest request)
        {
            Response response = new Response();
            try
            {
                response = await _trainDL.Create(request);
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
            GetAllTrainResponse response = new GetAllTrainResponse();
            try
            {
                response = await _trainDL.GetAll();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByID([FromQuery] string generateID)
        {
            GetTrainByIDResponse response = new GetTrainByIDResponse();
            try
            {
                response = await _trainDL.GetByID(generateID);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateByID(TrainInsertRequest request)
        {
            Response response = new Response();
            try
            {
                response = await _trainDL.UpdateByID(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteTrainRequest request)
        {
            Response response = new Response();
            try
            {
                response = await _trainDL.Delete(request);
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
