using MongoDB.Driver;
using Web.Model;

namespace Web.DataAccessLayer.Services
{
    public class TravellerDL : ITravellerDL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<TravellerInsertRequest> _mongoCollection;

        public TravellerDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            _mongoCollection = _MongoDatabase.GetCollection<TravellerInsertRequest>(_configuration["Database:TravellerCollectionName"]);
        }
        public async Task<Response> Create(TravellerInsertRequest request)
        {
            Response response = new Response();
            response.IsSuccess = true;
            response.Message = "Traveller Successfuly Created";
            try
            {
                request.JoinDate = DateTime.Now.ToShortDateString();
                await _mongoCollection.InsertOneAsync(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<Response> Delete(DeleteTravellerRequest request)
        {
            GetTravellerByNICResponse response1 = await GetByNIC(request.NIC);
            Response response = new Response();
            response.IsSuccess = true;
            response.Message = "Traveller Delete Successfully";
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

        public async Task<GetAllTravellerResponse> GetAll()
        {
            GetAllTravellerResponse response = new GetAllTravellerResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully";
            try
            {
                response.data = new List<TravellerInsertRequest>();
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

        public async Task<GetTravellerByNICResponse> GetByNIC(string nic)
        {
            GetTravellerByNICResponse response = new GetTravellerByNICResponse();
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

        public async Task<Response> UpdateByNIC(TravellerInsertRequest request)
        {
            Response response = new Response();
            response.IsSuccess = true;
            response.Message = "Record Update Successfully By NIC";
            try
            {
                GetTravellerByNICResponse response1 = await GetByNIC(request.NIC);
                request.JoinDate = response1.data.JoinDate;
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


    }
}
