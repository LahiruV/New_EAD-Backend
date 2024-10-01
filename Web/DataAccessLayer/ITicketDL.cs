using Web.Model;

namespace Web.DataAccessLayer
{
    public interface ITicketDL
    {
        public Task<Response> Create(TicketInsertRequest request);
        public Task<GetAllTicketResponse> GetAll();
        public Task<GetTicketByNICResponse> GetByNIC(string nic);
        public Task<Response> UpdateByNIC(TicketInsertRequest request);
        public Task<Response> Delete(DeleteTicketRequest request);
    }
}
