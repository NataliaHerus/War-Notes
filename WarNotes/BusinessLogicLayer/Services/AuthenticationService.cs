using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        protected readonly IUserService _userService;

        public AuthenticationService(IUserService userService)
        {
            _userService = userService;
        }

        public UserDetailDTO? CurrentAccount
        {
            get; set;
        }

        public UserDetailDTO Login(string email, string password)
        {
            UserDetailDTO storedAccount = _userService.GetUserByEmailAsync(email);

            if (storedAccount == null)
            {
                throw new UserNotFoundException("Користувача з вказаною поштою не існує", email);
            }

            Hasher storedHash = new Hasher(password);
            string storedHashedPasssword = storedHash.ComputeHash();
            Hasher hash = new Hasher(storedAccount.Password);
            string hashedPasssword = hash.ComputeHash();

            if (storedHashedPasssword != hashedPasssword)
            {
                throw new InvalidPasswordException("Неправильний пароль", email, password);
            }

            CurrentAccount = storedAccount;
            return storedAccount;
        }

        public bool IsLoggedIn()
        {
            if (CurrentAccount is null)
                return false;
            return true;
        }

        public void Logout()
        {
            CurrentAccount = null;
        }
    }
}
