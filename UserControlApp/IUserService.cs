using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace UserControlApp
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        Task<List<UserDTO>> GetAllUsersAsync();

        [OperationContract]
        Task<int> UpdateUserAsync(uint id, UserDTO user);

        [OperationContract]
        Task<int> DeleteUserAsync(uint id);

        [OperationContract]
        Task<int> AddUserAsync(UserDTO user);
    }
}
