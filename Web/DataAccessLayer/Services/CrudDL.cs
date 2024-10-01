using Amazon.Runtime.Internal;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Web.Model;

namespace Web.DataAccessLayer.Services
{
    public class CrudDL : ICrudDL
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertRecordRequest> _mongoCollection;

        public CrudDL (IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _MongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            _mongoCollection = _MongoDatabase.GetCollection<InsertRecordRequest>(_configuration["Database:CollectionName"]);
        }

        public async Task<InsertRecordResponse> InsertRecord(InsertRecordRequest request)
        {
            InsertRecordResponse response = new InsertRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Successfuly Inserted";
            try
            {
                request.CreateDate = DateTime.Now.ToString();
                request.UpdateDate = string.Empty;
                await _mongoCollection.InsertOneAsync(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<GetAllRecordResponse> GetAllRecord()
        {
            GetAllRecordResponse response = new GetAllRecordResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully";
            try
            {
                response.data = new List<InsertRecordRequest>();
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

        public async Task<GetRecordByIDResponse> GetRecordByID(string id)
        {
            GetRecordByIDResponse response = new GetRecordByIDResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully By ID";
            try
            {
                response.data = await _mongoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (response.data == null)
                {
                    response.Message = "Invalid Id";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }  
        public async Task<GetRecordByNameResponse> GetRecordByName(string name)
        {
            GetRecordByNameResponse response = new GetRecordByNameResponse();
            response.IsSuccess = true;
            response.Message = "Data Fetch Successfully By Name";
            try
            {
                response.data = new List<InsertRecordRequest>();
                response.data = await _mongoCollection.Find(x => (x.FirstName == name) || (x.LastName == name)).ToListAsync();
                if (response.data.Count == 0)
                {
                    response.Message = "Invalid Name";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<UpdateRecordByIDResponse> UpdateRecordByID(InsertRecordRequest request)
        {
            UpdateRecordByIDResponse response = new UpdateRecordByIDResponse();
            response.IsSuccess = true;
            response.Message = "Record Update Successfully By ID";
            try
            {
                GetRecordByIDResponse response1 = await GetRecordByID(request.Id);
                request.CreateDate = response1.data.CreateDate;
                request.UpdateDate = DateTime.Now.ToString();
                var Result = await _mongoCollection.ReplaceOneAsync(x=>x.Id == request.Id,request);

                if(!Result.IsAcknowledged)
                {
                    response.Message = "Input Id Not Found / Updation No Occurs";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<UpdateSalaryByIdResponse> UpdateSalaryById(UpdateSalaryByIdRequest request)
        {
            UpdateSalaryByIdResponse response = new UpdateSalaryByIdResponse();
            response.IsSuccess = true;
            response.Message = "Record Update Successfully By ID";
            try
            {
                var Filter = new BsonDocument().Add("Salary",request.Salary).Add("UpdateDate",DateTime.Now.ToString());
                var UpdateDate = new BsonDocument("$set", Filter);
                var Result = await _mongoCollection.UpdateOneAsync(x => x.Id == request.Id, UpdateDate);

                if (!Result.IsAcknowledged)
                {
                    response.Message = "Input Id Not Found / Updation No Occurs";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<DeleteRecordByIDResponse> DeleteRecordByID(DeleteRecordByIDRequest request)
        {
            DeleteRecordByIDResponse response = new DeleteRecordByIDResponse();
            response.IsSuccess = true;
            response.Message = "Record Delete Successfully By ID";
            try
            {                
                var Result = await _mongoCollection.DeleteOneAsync(x => x.Id == request.Id);

                if (!Result.IsAcknowledged)
                {
                    response.Message = "Document Not Found In Collection, Please Enter valid ID";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Exception Occurs : " + ex.Message;
            }
            return response;
        }

        public async Task<DeleteAllRecordResponse> DeleteAllRecord()
        {
            DeleteAllRecordResponse response = new DeleteAllRecordResponse();
            response.IsSuccess = true;
            response.Message = "All Record Delete Successfully";
            try
            {
                var Result = await _mongoCollection.DeleteManyAsync(x => true);

                if (!Result.IsAcknowledged)
                {
                    response.Message = "Document Not Found In Collection";
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
