using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Authentication;
using DataAccessLayer.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Authentication
{

    public class Authenticator : IAuthenticator
    {
        private readonly IAuthenticationService _authenticationService;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public UserDetailDTO CurrentAccount
        {
            get => _authenticationService.CurrentAccount;
            set { }
        }

        public bool IsLoggedIn => _authenticationService.IsLoggedIn();

        public UserDetailDTO Login(string email, string password)
        {
            return _authenticationService.Login(email, password);
        }

        public void Logout()
        {
            _authenticationService.Logout();
        }
    }
}
