using DataAccessLayer.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        User UpdateUser(User dto);
        User GetUserByEmailAsync(string email);
        List<User> GetAllUsersListAsync();
        User GetUserById(int id);
        Task<int> SaveChangesAcync();
    }
}
