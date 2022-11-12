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
        UserDetailDTO CurrentAccount { get; }
        bool IsLoggedIn { get; }
        UserDetailDTO Login(string username, string password);

        void Logout();
    }
}
