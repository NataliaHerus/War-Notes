using AutoMapper;
using BusinessLogicLayer.DTO;
using DataAccessLayer.EntityFramework.Entities;

namespace BusinessLogicLayer
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<User, UserDetailDTO>().ReverseMap();
        }
    }
}
