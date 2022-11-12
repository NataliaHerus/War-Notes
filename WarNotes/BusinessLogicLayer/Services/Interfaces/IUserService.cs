using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserRegistrationDTO> CreateUserAsync(UserRegistrationDTO query);
        UserDetailDTO GetUserByEmailAsync(string email);
    }
}
