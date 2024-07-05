using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace UserControlApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
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
