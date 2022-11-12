using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.DTO;
using DataAccessLayer.EntityFramework.Entities;

namespace BusinessLogicLayer
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserRegistrationDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Article, ArticleDTO>().ReverseMap();
        }
    }
}
