using BusinessLogicLayer.DTO;
using DataAccessLayer.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Authentication
{
    public interface IAuthenticator
    {
        UserDetailDto? CurrentAccount { get; }
        bool IsLoggedIn { get; }
        UserDetailDto Login(string email, string password);

        void Logout();
    }
}
