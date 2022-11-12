using DataAccessLayer.EntityFramework.Entities;
using System.Collections.Generic;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        IEnumerable<string> GetArticleHeadersByCategoryName(string categoryName);
    }
}
