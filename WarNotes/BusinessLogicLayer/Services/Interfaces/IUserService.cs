﻿using BusinessLogicLayer.DTO;
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
        Task<UserDetailDTO> CreateUserAsync(UserDetailDTO dto);
        void UpdateUser(UserDetailDTO dto);
        UserDetailDTO GetUserByEmailAsync(string email);
        List<UserDetailDTO> GetAllUsersListAsync();
    }
}
