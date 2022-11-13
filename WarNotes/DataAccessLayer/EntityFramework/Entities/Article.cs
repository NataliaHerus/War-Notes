using System;

namespace DataAccessLayer.EntityFramework.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
