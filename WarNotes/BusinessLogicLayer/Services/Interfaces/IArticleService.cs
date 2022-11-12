using System.Collections.Generic;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IArticleService
    {
        IEnumerable<string> GetArticleHeadersByCategoryName(string categoryName);
    }
}
