using BusinessLogicLayer.DTO;
using DataAccessLayer.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailDto> CreateUserAsync(UserDetailDto dto);
        void UpdateUser(UserDetailDto dto);
        UserDetailDto GetUserByEmail(string email);
        UserDetailDto GetUserById(int id);
        List<UserDetailDto> GetAllUsersList();
    }
}
