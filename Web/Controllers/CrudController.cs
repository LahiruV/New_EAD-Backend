using Web.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Model;
using Amazon.Runtime.Internal;


namespace Web.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ICrudDL _crudDL;

        public CrudController(ICrudDL crudDL)
        {
            _crudDL = crudDL;
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord(InsertRecordRequest request)
        {
            InsertRecordResponse response = new InsertRecordResponse();
            try
            {
                response = await _crudDL.InsertRecord(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecord()
        {
            GetAllRecordResponse response = new GetAllRecordResponse();
            try
            {
                response = await _crudDL.GetAllRecord();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecordByID([FromQuery]string id)
        {
            GetRecordByIDResponse response = new GetRecordByIDResponse();
            try
            {
                response = await _crudDL.GetRecordByID(id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetRecordByName([FromQuery] string name)
        {
            GetRecordByNameResponse response = new GetRecordByNameResponse();
            try
            {
                response = await _crudDL.GetRecordByName(name);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        } 
        
        [HttpPut]
        public async Task<IActionResult> UpdateRecordByID(InsertRecordRequest request)
        {
            UpdateRecordByIDResponse response = new UpdateRecordByIDResponse();
            try
            {
                response = await _crudDL.UpdateRecordByID(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateSalaryById(UpdateSalaryByIdRequest request)
        {
            UpdateSalaryByIdResponse response = new UpdateSalaryByIdResponse();
            try
            {
                response = await _crudDL.UpdateSalaryById(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRecordByID(DeleteRecordByIDRequest request)
        {
            DeleteRecordByIDResponse response = new DeleteRecordByIDResponse();
            try
            {
                response = await _crudDL.DeleteRecordByID(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllRecord()
        {
            DeleteAllRecordResponse response = new DeleteAllRecordResponse();
            try
            {
                response = await _crudDL.DeleteAllRecord();
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
