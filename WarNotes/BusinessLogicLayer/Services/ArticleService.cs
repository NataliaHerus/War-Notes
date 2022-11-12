using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IMapper mapper, IArticleRepository articleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public IEnumerable<string> GetArticleHeadersByCategoryName(string categoryName)
        {
            return _articleRepository.GetArticleHeadersByCategoryName(categoryName);
        }

        public ArticleDTO GetArticleByTitle(string title, int categoryId)
        {
            var article = _articleRepository.GetArticleByTitle(title, categoryId);
            return _mapper.Map<ArticleDTO>(article);
        }
    }
}
