using BusinessLogicLayer.DTO;
using DataAccessLayer.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthenticationService
    {
        UserDetailDTO Login(string email, string password);
        UserDetailDTO? CurrentAccount { get; set; }
        bool IsLoggedIn();

        void Logout();
    }
}
