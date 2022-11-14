using DataAccessLayer.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        IEnumerable<string> GetArticleHeadersByCategoryName(string categoryName);
        string GetArticleTitleById(int id);
        Article GetArticleByTitle(string title, int categoryId);

        bool ArticleIsLikedByUserId(int userId, int articleId);
        bool ArticleIsSavedByUserId(int userId, int articleId);

        void AddLikedArticle(int userId, int articleId);
        void AddSavedArticle(int userId, int articleId);

        void DeleteLikedArticle(int userId, int articleId);
        void DeleteSavedArticle(int userId, int articleId);
        IEnumerable<Article> GetLikedArticlesByUserId(int userId);
        IEnumerable<Article> GetSavedArticlesByUserId(int userId);
    }
}
