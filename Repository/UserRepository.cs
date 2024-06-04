using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Store325953446Context _store325953446Context;

        public UserRepository(Store325953446Context store325953446Context)
        {
            _store325953446Context = store325953446Context;
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string password)
        {
             User user = await _store325953446Context.Users.Where(user => user.UserName == email && user.Password == password).FirstOrDefaultAsync();
             return user;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _store325953446Context.Users.FindAsync(id);
            
        }

        public async Task<User> AddUser(User user)
        {
            await _store325953446Context.Users.AddAsync(user);
            await _store325953446Context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(int id, User upUser)
        {
            _store325953446Context.Update(upUser);
            await _store325953446Context.SaveChangesAsync();
            return upUser;
        }
    }
}



