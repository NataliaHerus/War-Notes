using AutoMapper;
using DataAccessLayer.EntityFramework.Entities;
using BusinessLogicLayer.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRegistrationDTO>().ReverseMap();
        }
    }
}
