using BusinessLogicLayer.DTO;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IArticleService
    {
        IEnumerable<string> GetArticleHeadersByCategoryName(string categoryName);
        ArticleDto GetArticleByTitle(string title, int categoryId);
        string GetArticleTitleById(int id);

        bool ArticleIsLikedByUserId(int userId, int articleId);
        bool ArticleIsSavedByUserId(int userId, int articleId);

        void AddLikedArticle(int userId, int articleId);
        void AddSavedArticle(int userId, int articleId);

        void DeleteLikedArticle(int userId, int articleId);
        void DeleteSavedArticle(int userId, int articleId);
        IEnumerable<ArticleDto> GetLikedArticlesByUserId(int userId);
        IEnumerable<ArticleDto> GetSavedArticlesByUserId(int userId);

        int GetCountOfLikes(int articleId);
        int GetCountOfSaves(int articleId);
    }
}
