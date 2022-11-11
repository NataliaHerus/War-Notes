using AutoMapper;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.EntityFramework.Entities;
using DataAccessLayer.Repositories.Interfaces;
using BusinessLogicLayer.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EntityFramework;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        protected readonly WarNotesContext _dbContext;
        protected readonly IRepository<User> _userRepository;
        protected readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper, WarNotesContext dbContext)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<UserRegistrationDTO> CreateUserAsync(UserRegistrationDTO dto)
        { 
            var user = _mapper.Map<User>(dto);
            user.IsBlocked = false;
            user.Role = 1;
            var newUser = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAcync();

            return _mapper.Map<UserRegistrationDTO>(newUser);
        }

        public UserRegistrationDTO GetUserByEmailAsync(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);

            return _mapper.Map<UserRegistrationDTO>(user); 
        }
    }
}
