using Web.Model;

namespace Web.DataAccessLayer
{
    public interface IUserDL
    {
        public Task<Response> CreateUser(UserInsertRequest request);
        public Task<GetAllUserResponse> GetAll();
    }
}
