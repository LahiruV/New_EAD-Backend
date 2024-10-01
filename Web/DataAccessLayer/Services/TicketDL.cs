using MongoDB.Driver;
using Web.Model;

namespace Web.DataAccessLayer.Services
{
    public class TicketDL : ITicketDL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<TicketInsertRequest> _mongoCollection;

        public TicketDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            _mongoCollection = _MongoDatabase.GetCollection<TicketInsertRequest>(_configuration["Database:TicketCollectionName"]);
        }
        public async Task<Response> Create(TicketInsertRequest request)
        {
            Response response = new Response();
            response.IsSuccess = true;
            response.Message = "Reservation Successfuly Created";
            try
            {
                request.CreateDate = DateTime.Now.ToShortDateString();
                await _mongoCollection.InsertOneAsync(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<GetAllTicketResponse> GetAll()
        {
            GetAllTicketResponse response = new GetAllTicketResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully";
            try
            {
                response.data = new List<TicketInsertRequest>();
                response.data = await _mongoCollection.Find(x => true).ToListAsync();
                if (response.data.Count == 0)
                {
                    response.Message = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<GetTicketByNICResponse> GetByNIC(string nic)
        {
            GetTicketByNICResponse response = new GetTicketByNICResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully By NIC";
            try
            {
                response.data = await _mongoCollection.Find(x => x.NIC == nic).FirstOrDefaultAsync();
                if (response.data == null)
                {
                    response.Message = "Invalid NIC";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<Response> UpdateByNIC(TicketInsertRequest request)
        {
            Response response = new Response();
            response.IsSuccess = true;
            response.Message = "Record Update Successfully By NIC";
            try
            {
                GetTicketByNICResponse response1 = await GetByNIC(request.NIC);
                request.CreateDate = response1.data.CreateDate;
                var Result = await _mongoCollection.ReplaceOneAsync(x => x.NIC == request.NIC, request);

                if (!Result.IsAcknowledged)
                {
                    response.Message = "Input NIC Not Found / Updation No Occurs";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<Response> Delete(DeleteTicketRequest request)
        {
            GetTicketByNICResponse response1 = await GetByNIC(request.NIC);
            Response response = new Response();
            response.IsSuccess = true;
            response.Message = "Reservation Delete Successfully";
            try
            {
                var Result = await _mongoCollection.DeleteOneAsync(x => x.Id == response1.data.Id);

                if (!Result.IsAcknowledged)
                {
                    response.Message = "Document Not Found In Collection, Please Enter valid NIC";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }


    }
}
