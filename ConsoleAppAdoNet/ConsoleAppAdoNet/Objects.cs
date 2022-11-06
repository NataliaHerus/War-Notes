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

        public override string ToString()
        {
            return $"User: Id: {Id}\t  FirstName: {FirstName}\tLastName: {LastName}\tEmail: {Email}\tPassword: {Password}\tRole: {Role}\tIs blocked: {IsBlocked}\n";
        }
    }

    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public override string ToString()
        {
            return $"Category: Id: {Id} \t\tCategoryName: {CategoryName}";
        }
    }

    public class Article
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EditedAt { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }

        public override string ToString()
        {
            return $"Article: Id: {Id}\t  CreatedAt: {CreatedAt}\t\tEditedAt: {EditedAt}\tText: {Text}\tCategoryId: {CategoryId}";
        }
    }

    public class LikedArticles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }


        public override string ToString()
        {
            return $"LikedArticles: Id: {Id}\tUserId: {UserId}\tArticleId: {ArticleId}";
        }
    }

    public class SavedArticles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }

        public override string ToString()
        {
            return $"SavedArticles: Id: {Id}\tUserId: {UserId}\tArticleId: {ArticleId}";
        }
    }
}
