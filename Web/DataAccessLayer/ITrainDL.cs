using Web.Model;

namespace Web.DataAccessLayer
{
    public interface ITrainDL
    {
        public Task<Response> Create(TrainInsertRequest request);
        public Task<GetAllTrainResponse> GetAll();
        public Task<GetTrainByIDResponse> GetByID(string generateID);
        public Task<Response> UpdateByID(TrainInsertRequest request);
        public Task<Response> Delete(DeleteTrainRequest request);
    }
}
