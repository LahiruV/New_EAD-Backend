using MongoDB.Driver;
using Web.Model;

namespace Web.DataAccessLayer.Services
{
    public class UserDL : IUserDL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<UserInsertRequest> _mongoCollection;

        public UserDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            _mongoCollection = _MongoDatabase.GetCollection<UserInsertRequest>(_configuration["Database:UserCollectionName"]);
        }

        public async Task<Response> CreateUser(UserInsertRequest request)
        {
            Response response = new Response();
            response.IsSuccess = true;
            response.Message = "User Successfuly Created";
            try
            {
                request.JoinDate = DateTime.Now.ToString();
                await _mongoCollection.InsertOneAsync(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<GetAllUserResponse> GetAll()
        {
            GetAllUserResponse response = new GetAllUserResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully";
            try
            {
                response.data = new List<UserInsertRequest>();
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
    }
}
