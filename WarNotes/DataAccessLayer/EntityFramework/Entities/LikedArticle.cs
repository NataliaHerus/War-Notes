namespace DataAccessLayer.EntityFramework.Entities
{
    public class LikedArticle
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int ArticleId { get; set; }
        public Article? Article { get; set; }
    }
}
