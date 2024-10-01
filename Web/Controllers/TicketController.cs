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
    public class TicketController : ControllerBase
    {
        private readonly ITicketDL _ticketDL;

        public TicketController(ITicketDL ticketDL)
        {
            _ticketDL = ticketDL;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketInsertRequest request)
        {
            Response response = new Response();
            try
            {
                response = await _ticketDL.Create(request);
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
            GetAllTicketResponse response = new GetAllTicketResponse();
            try
            {
                response = await _ticketDL.GetAll();
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
            GetTicketByNICResponse response = new GetTicketByNICResponse();
            try
            {
                response = await _ticketDL.GetByNIC(nic);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateByNIC(TicketInsertRequest request)
        {
            Response response = new Response();
            try
            {
                response = await _ticketDL.UpdateByNIC(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteTicketRequest request)
        {
            Response response = new Response();
            try
            {
                response = await _ticketDL.Delete(request);
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
