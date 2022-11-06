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

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        protected readonly IRepository<User> _userRepository;
        protected readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            user.IsBlocked = false;
            user.Role = 1;
            var newUser = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAcync();

            return _mapper.Map<UserDTO>(newUser);
        }

        public Task<UserDTO> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
