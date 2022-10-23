using DataAccessLayer.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class WarNotesContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SavedArticle> SavedArticles { get; set; }
        public DbSet<LikedArticle> LikedArticles { get; set; }

        public WarNotesContext()
        {

        }

        public WarNotesContext(DbContextOptions<WarNotesContext> options)
            : base(options)
        {

        }
    }
}
