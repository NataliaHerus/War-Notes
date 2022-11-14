using DataAccessLayer.EntityFramework;
using DataAccessLayer.EntityFramework.Entities;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        public ArticleRepository(WarNotesContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<string> GetArticleHeadersByCategoryName(string categoryName)
        {
            return _dbContext
                .Articles
                .Include(a => a.Category)
                .Where(a => a.Category.CategoryName == categoryName)
                .Select(a => a.Title);
        }

        public string GetArticleTitleById(int id)
        {
            return _dbContext
                .Articles
                .First(a => a.Id == id)
                .Title;
        }

        public Article GetArticleByTitle(string title, int categoryId)
        {
            return _dbContext
                .Articles
                .First(a => a.CategoryId == categoryId && a.Title == title);
        }

        public bool ArticleIsLikedByUserId(int userId, int articleId)
        {
            return _dbContext
                .LikedArticles
                .FirstOrDefault(a => a.UserId == userId && a.ArticleId == articleId) != null;
        }

        public bool ArticleIsSavedByUserId(int userId, int articleId)
        {
            return _dbContext
                .SavedArticles
                .FirstOrDefault(a => a.UserId == userId && a.ArticleId == articleId) != null;
        }

        public void AddLikedArticle(int userId, int articleId)
        {
            LikedArticle lickedArticle = new LikedArticle
            {
                UserId = userId,
                ArticleId = articleId
            };

            _dbContext.LikedArticles.Add(lickedArticle);
            _dbContext.SaveChanges();
        }

        public void AddSavedArticle(int userId, int articleId)
        {
            SavedArticle savedArticle = new SavedArticle
            {
                UserId = userId,
                ArticleId = articleId
            };

            _dbContext.SavedArticles.Add(savedArticle);
            _dbContext.SaveChanges();
        }

        public void DeleteLikedArticle(int userId, int articleId)
        {
            LikedArticle likedArticle = _dbContext.LikedArticles.First(a => a.UserId == userId && a.ArticleId == articleId);
            _dbContext.LikedArticles.Remove(likedArticle);
            _dbContext.SaveChanges();
        }

        public void DeleteSavedArticle(int userId, int articleId)
        {
            SavedArticle savedArticle = _dbContext.SavedArticles.First(a => a.UserId == userId && a.ArticleId == articleId);
            _dbContext.SavedArticles.Remove(savedArticle);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Article> GetLikedArticlesByUserId(int userId)
        {
            return _dbContext
                .LikedArticles
                .Where(a => a.UserId == userId).Select(b => b.Article).ToList();
        }

        public IEnumerable<Article> GetSavedArticlesByUserId(int userId)
        {
            return _dbContext
               .SavedArticles
               .Where(a => a.UserId == userId).Select(b => b.Article).ToList();
        }
    }
}
