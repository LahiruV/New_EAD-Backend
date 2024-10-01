using Web.Model;

namespace Web.DataAccessLayer
{
    public interface ITravellerDL
    {
        public Task<Response> Create(TravellerInsertRequest request);
        public Task<GetAllTravellerResponse> GetAll();
        public Task<GetTravellerByNICResponse> GetByNIC(string nic);
        public Task<Response> UpdateByNIC(TravellerInsertRequest request);
        public Task<Response> Delete(DeleteTravellerRequest request);

    }
}
