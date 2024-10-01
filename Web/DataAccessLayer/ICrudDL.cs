using Web.Model;

namespace Web.DataAccessLayer
{
    public interface ICrudDL
    {
        public Task<InsertRecordResponse> InsertRecord(InsertRecordRequest request);
        public Task<GetAllRecordResponse> GetAllRecord();
        public Task<GetRecordByIDResponse> GetRecordByID(string id);
        public Task<GetRecordByNameResponse> GetRecordByName(string name);
        public Task<UpdateRecordByIDResponse> UpdateRecordByID(InsertRecordRequest request);
        public Task<UpdateSalaryByIdResponse> UpdateSalaryById(UpdateSalaryByIdRequest request);
        public Task<DeleteRecordByIDResponse> DeleteRecordByID(DeleteRecordByIDRequest request);
        public Task<DeleteAllRecordResponse> DeleteAllRecord();
    }
}
