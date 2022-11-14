using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.EntityFramework.Entities;

namespace BusinessLogicLayer
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<User, UserDetailDto>().ReverseMap();
        }
    }
}
