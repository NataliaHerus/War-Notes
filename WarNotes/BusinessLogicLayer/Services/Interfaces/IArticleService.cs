using BusinessLogicLayer.DTO;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IArticleService
    {
        IEnumerable<string> GetArticleHeadersByCategoryName(string categoryName);
        ArticleDTO GetArticleByTitle(string title, int categoryId);
    }
}
