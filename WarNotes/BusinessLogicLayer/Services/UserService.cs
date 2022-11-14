using AutoMapper;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.EntityFramework.Entities;
using DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.EntityFramework;
using BusinessLogicLayer.DTO;

namespace BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        protected readonly WarNotesContext? _dbContext;
        protected readonly IUserRepository _userRepository;
        protected readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserDetailDto> CreateUserAsync(UserDetailDto dto)
        { 
            var user = _mapper.Map<User>(dto);
            user.IsBlocked = false;
            user.Role = 1;
            var newUser = await _userRepository.CreateUserAsync(user);
            await _userRepository.SaveChangesAcync();

            return _mapper.Map<UserDetailDto>(newUser);
        }

        public UserDetailDto GetUserByEmail(string email)
        {
            var user = _userRepository.GetUserByEmailAsync(email);
            return _mapper.Map<UserDetailDto>(user); 
        }

        public void UpdateUser(UserDetailDto dto)
        {
            var user = _userRepository.GetUserById(dto.Id);
            _userRepository.UpdateUser(user);
             _mapper.Map(dto, user);
            _userRepository.SaveChangesAcync();
        }

        public List<UserDetailDto> GetAllUsersList()
        {
            var users = _userRepository.GetAllUsersListAsync();
            return _mapper.Map<List<UserDetailDto>>(users);
        }

        public UserDetailDto GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            return _mapper.Map<UserDetailDto>(user);
        }
    }
}
