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
        protected readonly WarNotesContext _dbContext;
        protected readonly IRepository<User> _userRepository;
        protected readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IMapper mapper, WarNotesContext dbContext)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<UserDetailDTO> CreateUserAsync(UserDetailDTO dto)
        { 
            var user = _mapper.Map<User>(dto);
            user.IsBlocked = false;
            user.Role = 1;
            var newUser = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAcync();

            return _mapper.Map<UserDetailDTO>(newUser);
        }

        public UserDetailDTO GetUserByEmailAsync(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Email == email);
            return _mapper.Map<UserDetailDTO>(user); 
        }

        public void UpdateUser(UserDetailDTO dto)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == dto.Id);
             _mapper.Map(dto, user);
            _userRepository.SaveChangesAcync();
        }

        public List<UserDetailDTO> GetAllUsersListAsync()
        {
            var users = _dbContext.Users.ToList();
            return _mapper.Map<List<UserDetailDTO>>(users);
        }
    }
}
