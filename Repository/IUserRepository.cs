using Entities.Models;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetUserByEmailAndPassword(string email, string password);
        Task<User> GetUserById(int id);
        Task<User> UpdateUser(int id, User upUser);
    }
}