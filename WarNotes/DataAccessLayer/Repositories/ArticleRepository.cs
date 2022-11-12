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
    }
}
