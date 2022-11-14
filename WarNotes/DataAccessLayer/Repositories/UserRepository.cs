using DataAccessLayer.EntityFramework;
using DataAccessLayer.EntityFramework.Entities;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly WarNotesContext _dbContext;
        public UserRepository(WarNotesContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            await _dbContext.Users!.AddAsync(user);
            return user;
        }

        public List<User> GetAllUsersListAsync()
        {
            return _dbContext.Users!.ToList();
        }

        public User GetUserByEmailAsync(string email)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Email == email);
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public User UpdateUser(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            return user;
        }

        public async Task<int> SaveChangesAcync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
