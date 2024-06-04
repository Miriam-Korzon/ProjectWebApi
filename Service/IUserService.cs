using Entities.Models;

namespace Services
{
    public interface IUserService
    {
        Task<User> AddUser(User user);
        Task<int> CheckPwd(string password);
        Task<User> GetUserByEmailAndPassword(string email, string password);
        Task<User> GetUserById(int id);
        Task<User> UpdateUser(int id, User user);
    }
}