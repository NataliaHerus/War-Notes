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

        public string GetArticleTitleById(int id)
        {
            return _articleRepository.GetArticleTitleById(id);
        }

        public ArticleDTO GetArticleByTitle(string title, int categoryId)
        {
            var article = _articleRepository.GetArticleByTitle(title, categoryId);
            return _mapper.Map<ArticleDTO>(article);
        }

        public bool ArticleIsLikedByUserId(int userId, int articleId)
        {
            return _articleRepository.ArticleIsLikedByUserId(userId, articleId);
        }

        public bool ArticleIsSavedByUserId(int userId, int articleId)
        {
            return _articleRepository.ArticleIsSavedByUserId(userId, articleId);
        }

        public void AddLikedArticle(int userId, int articleId)
        {
            _articleRepository.AddLikedArticle(userId, articleId);
        }

        public void DeleteLikedArticle(int userId, int articleId)
        {
            _articleRepository.DeleteLikedArticle(userId, articleId);
        }

        public void AddSavedArticle(int userId, int articleId)
        {
            _articleRepository.AddSavedArticle(userId, articleId);
        }

        public void DeleteSavedArticle(int userId, int articleId)
        {
            _articleRepository.DeleteSavedArticle(userId, articleId);
        }

        public IEnumerable<ArticleDTO> GetLikedArticlesByUserId(int userId)
        {
            var likedArticles = _articleRepository.GetLikedArticlesByUserId(userId);
            return _mapper.Map<IEnumerable<ArticleDTO>>(likedArticles);
        }

        public IEnumerable<ArticleDTO> GetSavedArticlesByUserId(int userId)
        {
            var savedArticles = _articleRepository.GetSavedArticlesByUserId(userId);
            return _mapper.Map<IEnumerable<ArticleDTO>>(savedArticles);
        }

        public int GetCountOfLikes(int articleId)
        {
            return _articleRepository.GetCountOfLikes(articleId);
        }

        public int GetCountOfSaves(int articleId)
        {
            return _articleRepository.GetCountOfSaves(articleId);
        }
    }
}
