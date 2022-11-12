using DataAccessLayer.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class ArticleDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
