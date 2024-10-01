using MongoDB.Driver;
using Web.Model;

namespace Web.DataAccessLayer.Services
{
    public class TrainDL : ITrainDL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<TrainInsertRequest> _mongoCollection;

        public TrainDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            _mongoCollection = _MongoDatabase.GetCollection<TrainInsertRequest>(_configuration["Database:TrainCollectionName"]);
        }
        public async Task<Response> Create(TrainInsertRequest request)
        {
            Response response = new Response();
            response.IsSuccess = true;
            response.Message = "Train Shedule Successfuly Created";
            try
            {
                await _mongoCollection.InsertOneAsync(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<GetAllTrainResponse> GetAll()
        {
            GetAllTrainResponse response = new GetAllTrainResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully";
            try
            {
                response.data = new List<TrainInsertRequest>();
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

        public async Task<GetTrainByIDResponse> GetByID(string generateID)
        {
            GetTrainByIDResponse response = new GetTrainByIDResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully By ID";
            try
            {
                response.data = await _mongoCollection.Find(x => x.GenerateID == generateID).FirstOrDefaultAsync();
                if (response.data == null)
                {
                    response.Message = "Invalid ID";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<Response> UpdateByID(TrainInsertRequest request)
        {
            Response response = new Response();
            response.IsSuccess = true;
            response.Message = "Record Update Successfully By ID";
            try
            {
                var Result = await _mongoCollection.ReplaceOneAsync(x => x.GenerateID == request.GenerateID, request);

                if (!Result.IsAcknowledged)
                {
                    response.Message = "Input ID Not Found / Updation No Occurs";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<Response> Delete(DeleteTrainRequest request)
        {
            GetTrainByIDResponse response1 = await GetByID(request.GenerateID);
            Response response = new Response();
            response.IsSuccess = true;
            response.Message = "Train Shedule Delete Successfully";
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
