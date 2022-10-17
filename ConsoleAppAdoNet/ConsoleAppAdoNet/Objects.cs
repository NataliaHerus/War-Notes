namespace ConsoleAppAdoNet
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public bool IsBlocked { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }

    public class Article
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EditedAt { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
    }

    public class LikedArticles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
    }

    public class SavedArticles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
    }
}
