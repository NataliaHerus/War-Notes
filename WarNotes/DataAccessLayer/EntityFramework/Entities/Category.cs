using System.Collections.Generic;

namespace DataAccessLayer.EntityFramework.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? CategoryName { get; set; }

        public IEnumerable<Article>? Articles { get; set; }
    }
}
